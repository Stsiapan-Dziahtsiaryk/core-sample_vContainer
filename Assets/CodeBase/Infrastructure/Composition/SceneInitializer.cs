using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Infrastructure.Composition
{
    // Perhaps, it isn't necessary instead of i should use the Child Scope with a IInstaller 
    public class SceneInitializer
    {
        private readonly LifetimeScope _container;

        public SceneInitializer(LifetimeScope container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public UniTask InitializeAsync()
        {
            SceneCompositionRoot[] compositionRoots = Object.FindObjectsOfType<SceneCompositionRoot>();

            if (compositionRoots.Length > 1)
            {
                throw new Exception($"Scene has multiple composition roots!" +
                                    " Must use only one composition root" +
                                    $" roots:{string.Join(",", compositionRoots.Select(root => root.name))}");
            }
            return compositionRoots[0].Initialize(_container);
        }
    }
}