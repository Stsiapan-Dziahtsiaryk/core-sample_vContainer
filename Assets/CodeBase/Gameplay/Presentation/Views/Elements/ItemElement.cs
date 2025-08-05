using CodeBase.Shared.Extends;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.Presentation.Views.Elements
{
    public class ItemElement : MonoBehaviour
    {
        public class Pool : MonoPool<ItemElement>
        {
            public Pool(LifetimeScope container, ItemElement prefab, int maxInstances = 10, string parentName = "Parent of Pool")
                : base(container, prefab, maxInstances, parentName) { }
        }
        
        [SerializeField] private Rigidbody2D _physicsBody;
        private float _speed;
        
        private void StartMove()
        {
            
        }
    }
}