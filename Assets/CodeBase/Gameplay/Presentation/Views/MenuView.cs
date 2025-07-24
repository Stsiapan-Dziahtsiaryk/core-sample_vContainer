using Shared.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Presentation.Views
{
    public class MenuView : CanvasGroupView
    {
        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public Button PrivacyButton { get; private set; }
    }
}