using Shared.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Presentation.Views
{
    public class SettingsView : CanvasGroupView
    {
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public Button MenuButton { get; private set; }
        [field: SerializeField] public Toggle SoundToggle { get; private set; }
        [field: SerializeField] public Toggle VibrationToggle { get; private set; }
    }
}