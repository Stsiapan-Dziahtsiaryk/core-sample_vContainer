using Shared.Presentation;
using UnityEngine;

namespace GraySide.Presentation.Views
{
    public class CurtainView : CanvasGroupView
    {
        [field: SerializeField] public ProgressElement ProgressElement { get; private set; }
    }
}