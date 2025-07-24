using Shared.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Domain.Presentation.Views
{
    public class TestView : CanvasGroupView
    {
        [field: SerializeField] public Button ChangeStateButton { get; private set; }
        [field: SerializeField] public Button CheckWindowButton { get; private set; }
    }
}