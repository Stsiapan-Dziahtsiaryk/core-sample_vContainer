using Shared.Presentation;
using UnityEngine;

namespace Domain.Presentation.Views
{
    public class CurtainView : CanvasGroupView
    {
        [field: SerializeField] public ProgressElement ProgressElement { get; private set; }
    }
}