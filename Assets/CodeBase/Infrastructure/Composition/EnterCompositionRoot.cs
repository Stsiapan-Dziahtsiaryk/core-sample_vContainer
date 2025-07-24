using System;
using System.Threading;
using CodeBase.Shared;
using CodeBase.Shared.StaticData;
using GraySide.Presentation.Presenters;
using GraySide.Presentation.Views;
using Infrastructure.Extensions;
using Infrastructure.StateMachine;
using Infrastructure.UIStateMachine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Composition
{
    // Entry point on the scene
    public class EnterCompositionRoot : IStartable, IDisposable
    {
        private readonly IObjectResolver _container;
        private CancellationTokenSource _cts;

        public EnterCompositionRoot(IObjectResolver container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Start()
        {
            _container
                .Bind<CurtainView, CurtainPresenter>()
                .Bind<InternetAlertView, InternetAlertPresenter>();

            _container
                .BindWindow<CurtainPresenter>(WindowType.Curtain)
                .BindWindow<InternetAlertPresenter>(WindowType.Alert);

            _container.Resolve<IWindowFsm>().Open(WindowType.Curtain, true);
            _container.Resolve<IAppStateMachine>().Enter(AppState.Gameplay);
        }

        public void Dispose()
        {
           
        }


    }
}