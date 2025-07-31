using UnityEngine;

namespace Gameplay.Presentation.Data.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Data/Configs/Gameplay/Create Game Config", order = 51)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxDeckSize { get; private set; }
    }
}