using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Composition
{
    public abstract class SceneCompositionRoot : MonoBehaviour
    {
        public abstract UniTask Initialize(LifetimeScope container);
    }
}