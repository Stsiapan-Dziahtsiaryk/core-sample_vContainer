using UnityEngine;

namespace Domain.Data.Configs
{
    [CreateAssetMenu(fileName = "App Settings", menuName = "Settings/App Settings", order = 51)]
    public class AppSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public bool ShowLogs { get; private set; }
    }
}
