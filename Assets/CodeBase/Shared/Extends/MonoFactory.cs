using System;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Shared.Extends
{
    public class MonoFactory<T>
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        protected readonly LifetimeScope _container;

        public MonoFactory(
            LifetimeScope container, 
            T prefab)
        {
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public virtual T Create()
        {
            return _container.Container.Instantiate(_prefab);
        }
    }
}