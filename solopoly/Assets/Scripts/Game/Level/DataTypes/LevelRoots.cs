using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RossoGames.Level.DataTypes
{
    [Serializable]
    public class LevelRoots
    {
        [field: SerializeField, BoxGroup("World")] public Transform Tiles { get; private set; }
        [field: SerializeField, BoxGroup("World")] public Transform Environment { get; private set; }
        [field: SerializeField, BoxGroup("World")] public Transform Tokens { get; private set; }
        [field: SerializeField, BoxGroup("World")] public Transform Buildings { get; private set; }

        [field: SerializeField, BoxGroup("UI")] public Transform Cards { get; private set; }
    }
}
