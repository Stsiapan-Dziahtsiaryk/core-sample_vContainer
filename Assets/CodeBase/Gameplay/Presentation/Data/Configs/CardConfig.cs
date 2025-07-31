using System;
using UnityEngine;

namespace Gameplay.Presentation.Data.Configs
{
    [Serializable]
    public struct CardConfig
    {
        public int ID;
        public Sprite Shirt;

        public CardConfig(int id, Sprite shirt)
        {
            ID = id;
            Shirt = shirt;
        }
    }
}