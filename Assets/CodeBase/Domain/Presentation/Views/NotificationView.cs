using Shared.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Domain.Presentation.Views
{
    public class NotificationView : CanvasGroupView
    {
        [field: SerializeField] public Button AcceptButton { get; private set; }
        [field: SerializeField] public Button SkipButton { get; private set; }
    }
}