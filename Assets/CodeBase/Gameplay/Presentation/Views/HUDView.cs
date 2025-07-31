using Shared.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Presentation.Views
{
    public class HUDView : CanvasGroupView
    {
        [field: SerializeField] public Button HomeButton { get; private set; } 
        [field: SerializeField] public Button RestartButton { get; private set; } 
        [field: SerializeField] public TMP_Text ScoreText { get; private set; }
    }
}