using System;
using UnityEngine;

namespace RossoGames.Progression.Data
{
    [Serializable]
    public class SaveData : IGameSave
    {
        [SerializeField] private int version;
        public bool Initialized;

        public int Version
        {
            get => version;
            set => version = value;
        }

        public SaveData()
        {
            Initialized = false;
        }
    }
}
