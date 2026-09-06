using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Rossogames.Level.DataContext
{
    [Serializable]
    public class LevelRootsDataContext
    {
        [field: SerializeField, BoxGroup("World")]
        public Transform Rooms { get; private set; }

        [field: SerializeField, BoxGroup("UI")]
        public Transform ContextActions { get; private set; }
    }
}
