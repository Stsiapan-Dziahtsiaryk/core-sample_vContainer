using System;
using CodeBase.Shared;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneProxy;
using Infrastructure.UIStateMachine;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private const string Scene = "Gameplay Scene";
        private readonly IWindowResolver _windowResolver;
        private readonly SceneLoader _sceneLoader;

        public GameplayState(
            IWindowResolver windowResolver,
            SceneLoader sceneLoader)
        {
            _windowResolver = windowResolver ?? throw new ArgumentNullException(nameof(windowResolver));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        public void Exit()
        {
        }

        public void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Gameplay entered");
#endif
            PreparedWindowFsm();

            _sceneLoader.LoadSceneAsync(Scene,  onLoaded:OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            // Anything additional 
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        private void PreparedWindowFsm()
        {
            _windowResolver.CleanUp();

            _windowResolver.Registering(WindowType.Curtain, new Window(WindowType.Curtain));
            _windowResolver.Registering(WindowType.Gameplay, new Window(WindowType.Gameplay));
            _windowResolver.Registering(WindowType.Settings, new Window(WindowType.Settings));
            _windowResolver.Registering(WindowType.Menu, new Window(WindowType.Menu));
            _windowResolver.Registering(WindowType.Popup, new Window(WindowType.Popup));
            _windowResolver.Registering(WindowType.Slots, new Window(WindowType.Slots));
            
        }
    }
}