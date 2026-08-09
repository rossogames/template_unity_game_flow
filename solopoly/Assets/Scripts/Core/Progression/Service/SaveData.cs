using Rossoforge.Core.UserData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RossoGames.Progression.Data
{
    [Serializable]
    public class SaveData : IGameSave
    {
        [SerializeField] private int version;
        public bool Initialized;
        public List<string> UnlockedBuildings;

        public int Version
        {
            get => version;
            set => version = value;
        }

        public SaveData()
        {
            UnlockedBuildings = new List<string>();
            Initialized = false;
        }
    }
}
