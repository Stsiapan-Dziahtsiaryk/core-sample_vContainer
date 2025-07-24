using Domain.Data.Configs;
using UnityEngine;

namespace Domain.Extensions
{
    public class LoggerService
    {
        private readonly bool _isShowing;

        public LoggerService(AppSettingsConfig settings)
        {
            _isShowing = settings.ShowLogs;
        }

        public void Print(string message)
        {
            if(_isShowing == false) return;
            Debug.Log("[Core]:" +message);
        }

        public void PrintWarning(string message)
        {
            if(_isShowing == false) return;
            Debug.LogWarning("[Core.Warning]:" + message);
        }
        
        public void PrintError(string message)
        {
            if(_isShowing == false) return;
            Debug.LogError("[Core.Error]:" + message);
        }
    }
}