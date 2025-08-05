using Gameplay.Presentation.Data;
using Gameplay.Presentation.Data.Configs;
using Gameplay.Presentation.Presenters;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Gameplay.Services;
using Infrastructure.Composition;
using Infrastructure.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private HUDView _hudView;
        [SerializeField] private PlayerView _playerView;
        
        [Header("Prefab's")]
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private ItemElement _itemPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // place where you register important things of gameplay state
            builder.RegisterEntryPoint<GameplayCompositionRoot>();
            builder.RegisterEntryPoint<InputService>().AsSelf();

            builder.RegisterPool<ItemElement, ItemElement.Pool>(_itemPrefab, "Items Pool");
            builder.RegisterInstance(_gameConfig);

            builder.RegisterInstance(_menuView);
            builder.RegisterInstance(_hudView);
            builder.RegisterInstance(_playerView);

            builder.Register<Game>(Lifetime.Singleton);
            
            builder.Register<HUDPresenter>(Lifetime.Singleton);
            builder.Register<MenuPresenter>(Lifetime.Singleton);
            builder.Register<PlayerPresenter>(Lifetime.Singleton);
        }
    } 
}