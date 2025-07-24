using Shared.Presentation;
using UnityEngine;

namespace Gameplay.Presentation.Views
{
    public class TableView : CanvasGroupView
    {
        [field: SerializeField] public RectTransform Content { get; private set; }
    }
}