using System;
using CodeBase.Shared;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneProxy;
using Infrastructure.UIStateMachine;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class EnterState : IState
    {
        private const string Scene = "Enter Scene";
        private readonly IWindowResolver _windowResolver;
        private readonly SceneLoader _sceneLoader;

        public EnterState(
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
            Debug.Log("Enter State entered");
            PreparedWindowFsm();
            
            _sceneLoader.LoadSceneAsync(Scene, onLoaded: OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            Debug.Log("Enter Scene loaded");
        }

        private void PreparedWindowFsm()
        {
            _windowResolver.CleanUp();

#if UNITY_EDITOR
            _windowResolver.Registering(WindowType.Invalid, new Window(WindowType.Invalid));
#endif
            _windowResolver.Registering(WindowType.Curtain, new Window(WindowType.Curtain));
            _windowResolver.Registering(WindowType.Alert, new Window(WindowType.Alert));
            _windowResolver.Registering(WindowType.Notification, new Window(WindowType.Notification));
        }
    }
}