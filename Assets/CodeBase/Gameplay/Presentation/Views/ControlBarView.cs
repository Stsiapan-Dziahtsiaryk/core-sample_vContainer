using Shared.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Presentation.Views
{
    public class ControlBarView : CanvasGroupView
    {
        [field: SerializeField] public Button StepBackButton { get; private set; }
        [field: SerializeField] public RectTransform DeckContainer { get; private set; }
        [field: SerializeField] public RectTransform Buffer { get; private set; }
    }
}