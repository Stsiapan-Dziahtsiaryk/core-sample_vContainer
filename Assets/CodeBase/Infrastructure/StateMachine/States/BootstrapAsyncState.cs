using System;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Shared;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneProxy;
using Infrastructure.UIStateMachine;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapAsyncState : IAsyncState
    {
        private readonly IAppStateMachine _stateMachine;
        private readonly WindowFsm _windowFsm;
        private readonly SceneLoader _sceneLoader;
        // ToDo: Add wrapper for WevView 

        public BootstrapAsyncState(
            IAppStateMachine stateMachine, 
            WindowFsm windowFsm,
            SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        public void Exit()
        {
            
        }

        public async Task EnterAsync(CancellationToken token)
        {
            PreparedWindowFsm();
            await UniTask.Delay(1, cancellationToken: token);
            // PreparedServices();
            // Debug.Log("Entered");
            // await _conversionDataService.InitializeAsync();
            // _webViewProvider.Initialize();
            // _webViewProvider.CreateView();

        }
        
        private void PreparedWindowFsm()
        {
            _windowFsm.CleanUp();
            
            _windowFsm.Registering(WindowType.Curtain, new Window(WindowType.Curtain));
            _windowFsm.Registering(WindowType.Alert, new Window(WindowType.Alert));
            _windowFsm.Registering(WindowType.Notification, new Window(WindowType.Notification));
        }

        private void PreparedServices()
        {
            // _notifyPushProvider.OnSend += () => _conversionDataService.Send(null);
            // _notifyPushProvider.OnOpenPermissionWindow += OnOpenPermission;
            // _notifyPushProvider.OnShowWebView += _webViewProvider.WebView
        }

        private void OnOpenPermission()
        {
            _windowFsm.Open(WindowType.Alert, true);
        }
    }
}