using Infrastructure.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    /// <summary>
    /// Entry point
    /// </summary>
    public class GameRunner : IStartable
    {
        private readonly IObjectResolver _container;

        public GameRunner(IObjectResolver container)
        {
            _container = container;
        }

        public void Start()
        {
            Application.targetFrameRate = 60;

            _container.Resolve<IStateResolver>().Initialize(_container);
        }
    }
}