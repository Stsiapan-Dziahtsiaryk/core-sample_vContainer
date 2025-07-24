using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneProxy
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<string> _loadedScenes = new List<string>();
        
        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded: onLoaded));

        public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }

        public async UniTask LoadSceneAsync(List<string> additionalScenes, Action onLoaded = null)
        { 
            List<string> unloadingScenes = GetSceneForUnloading(additionalScenes);
            _loadedScenes.AddRange(additionalScenes);
            List<UniTask> loadTasks = new List<UniTask>();

            foreach (string scene in unloadingScenes)
            {
                loadTasks.Add(UnloadSceneAsync(scene));
            }

            foreach (var scene in additionalScenes)
            {
                loadTasks.Add(LoadSceneAsync(scene, true));
            }

            await UniTask.WhenAll(loadTasks).ContinueWith(() => onLoaded?.Invoke());
        }
        
        public async UniTask LoadSceneAsync(string sceneName, List<string> additionalScenes = null, Action onLoaded = null)
        {
            
            List<UniTask> loadTasks = new List<UniTask>
            {
                LoadSceneAsync(sceneName, false)
            };
        
            if (additionalScenes != null)
            {
                foreach (var scene in additionalScenes)
                {
                    loadTasks.Add(LoadSceneAsync(scene, true));
                }
            }

            await UniTask.WhenAll(loadTasks).ContinueWith(() => onLoaded?.Invoke());
        }

        private List<string> GetSceneForUnloading(List<string> loadedScenes)
        {
            List<string> scenes = new List<string>(_loadedScenes);
            foreach (var loadedScene in loadedScenes)
            {
                scenes.Remove(loadedScene);
            }
            return scenes;
        }
        
        private bool IsCurrentScene(string sceneName) =>
            SceneManager.GetActiveScene().name == sceneName;
        
        private async UniTask LoadSceneAsync(string sceneName, bool additive)
        {
            if (IsCurrentScene(sceneName))
                return;

            await LoadSceneAsyncInternal(() => SceneManager.LoadSceneAsync(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single));
        }
        
        private async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.None);
        }
        
        private async UniTask LoadSceneAsyncInternal(Func<AsyncOperation> loadSceneFunc)
        {
            try
            {
                var asyncOperation = loadSceneFunc();
                asyncOperation.allowSceneActivation = false;

                while (asyncOperation.progress < 0.9f)
                {
                    await UniTask.Yield();
                }

                asyncOperation.allowSceneActivation = true;
                await asyncOperation;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading scene: {ex.Message}");
            }
        }
    }
}