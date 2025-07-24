using Gameplay.Presentation.Data;
using Gameplay.Presentation.Presenters;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Infrastructure.Composition;
using Infrastructure.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private TableView _tableView;
        [SerializeField] private ControlBarView _controlBarView;
        [SerializeField] private MenuView _menuView;
        
        [Header("Prefab's")]
        [SerializeField] private CardElement _cardPrefab;
        [SerializeField] private DeckElement _deckPrefab; 
        
        protected override void Configure(IContainerBuilder builder)
        {
            // place where you register important things of gameplay state
            builder.RegisterEntryPoint<GameplayCompositionRoot>();

            builder.RegisterPool<CardElement, CardElement.Pool>(_cardPrefab, "Cards Pool", 50);
            builder.RegisterPool<DeckElement, DeckElement.Pool>(_deckPrefab, "Deck Pool", 10);
            
            builder.RegisterInstance(_tableView);
            builder.RegisterInstance(_controlBarView);
            builder.RegisterInstance(_menuView);

            builder.Register<Table>(Lifetime.Singleton);

            builder.Register<TablePresenter>(Lifetime.Singleton);
            builder.Register<ControlBarPresenter>(Lifetime.Singleton);
            builder.Register<MenuPresenter>(Lifetime.Singleton);
        }
    } 
}