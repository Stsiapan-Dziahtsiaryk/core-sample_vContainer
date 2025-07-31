using System;
using CodeBase.Shared;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.Presenters;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Infrastructure.Extensions;
using Infrastructure.UIStateMachine;
using R3;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Composition
{
    public class GameplayCompositionRoot : IStartable, IDisposable, IGameHandler
    {
        private readonly IObjectResolver _container;
        private readonly ReactiveProperty<GameState> _state;
        
        public GameplayCompositionRoot(IObjectResolver container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _state = new ReactiveProperty<GameState>(GameState.Menu);
        }


        public ReadOnlyReactiveProperty<GameState> State => _state;

        public void Start()
        {
            _container.Resolve<DeckElement.Pool>().Start();
            _container.Resolve<Table>().Initialize();
            
            _container
                .Bind<MenuView, MenuPresenter>()
                .Bind<TableView, TablePresenter>()
                .Bind<ControlBarView, ControlBarPresenter>()
                .Bind<HUDView, HUDPresenter>();

            _container
                .BindWindow<MenuPresenter>(WindowType.Menu);
            
            _container.Resolve<IWindowFsm>().Open(WindowType.Menu);
        }

        public void Dispose()
        {
        }

        public void OnHandleNewGame()
        {
            _state.Value = GameState.Game;
            _state.ForceNotify();
            _container.Resolve<Table>().HandleNewGame();
            _container.Resolve<Game>().HandleNewGame();
        }

        public void OnChangeGameState(GameState state)
        {
            _state.Value = state;
        }
    }
}