using System;
using UnityEngine;
using AppState = CodeBase.Shared.StaticData.AppState;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IAppStateMachine _stateMachine;

        public BootstrapState(IAppStateMachine stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            Debug.Log("Bootstrap entered");
            // Make some additional configuration 
            Screen.orientation = ScreenOrientation.AutoRotation;

            _stateMachine.Enter(AppState.Enter);
        }
    }
}