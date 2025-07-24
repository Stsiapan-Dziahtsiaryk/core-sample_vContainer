using Gameplay.Audio;
using GraySide.Data.Configs;
using GraySide.Extensions;
using Infrastructure.Composition;
using Infrastructure.SceneProxy;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Infrastructure.UIStateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private AppSettingsConfig _appSettings;
        [SerializeField] private AudioConfig _audioConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_appSettings);
            builder.RegisterInstance(_audioConfig);

            builder.Register<LoggerService>(Lifetime.Singleton);
            
            builder.Register<AppStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WindowFsm>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SceneInitializer>(Lifetime.Singleton);
            builder.Register<Coroutines>(Lifetime.Singleton).As<ICoroutineRunner>();
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<AudioService>(Lifetime.Singleton);
            
            // domain
          
            
            // Binding states of app
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<EnterState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
            
            // Entry Point
            builder.RegisterEntryPoint<GameRunner>();
        }
    }
} 