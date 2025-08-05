using System;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class LevelPresenter : IPresenter
    {
        private readonly LevelView _view;
        private readonly ItemElement.Pool _itemPool;
        
        
        public LevelPresenter(
            LevelView view,
            ItemElement.Pool itemPool)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _itemPool = itemPool ?? throw new ArgumentNullException(nameof(itemPool));
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }
    }
}