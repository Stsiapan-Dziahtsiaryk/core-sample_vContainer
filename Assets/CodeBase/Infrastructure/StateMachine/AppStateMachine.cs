using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Shared.StaticData;
using Infrastructure.StateMachine.States;
using UnityEngine;
using VContainer;

namespace Infrastructure.StateMachine
{
    public class AppStateMachine :
        IAppStateMachine,
        IStateResolver
    {
        private readonly Dictionary<AppState, IExitableState> _states = new Dictionary<AppState, IExitableState>();
        private IExitableState _current;
        private CancellationTokenSource _cts;
        
        public AppStateMachine()
        {
        }

        public void Initialize(IObjectResolver resolver)
        {
            AddState(AppState.Bootstrap, resolver.Resolve<BootstrapState>());
            AddState(AppState.Enter, resolver.Resolve<EnterState>());
            AddState(AppState.Gameplay, resolver.Resolve<GameplayState>());
            Enter(AppState.Bootstrap);
        }

        public void AddState(AppState type, IExitableState state)
        {
            if (_states.TryAdd(type, state))
            {
#if UNITY_EDITOR
                Debug.Log($"State {type} was added successfully!");
#endif
                return;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"State {type} was already added!");
#endif
            }
        }

        public void Enter(AppState type)
        {
            if (type == AppState.Invalid) return;

            IState state = ChangedState<IState>(type);
            state?.Enter();
        }

        public async void EnterAsync(AppState type)
        {
            try
            {
                _cts = new CancellationTokenSource();
                IAsyncState state = ChangedState<IAsyncState>(type);
                await state?.EnterAsync(_cts.Token)!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IExitableState ChangedState(AppState type)
        {
            _current?.Exit();
            _current = _states[type];
            return _current;
        }

        private TState ChangedState<TState>(AppState type)
            where TState : class, IExitableState
        {
            _current?.Exit();
            _current = _states[type];
            return _current as TState;
        }
    }
}