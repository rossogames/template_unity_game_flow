using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RossoGames.Level.DataTypes
{
    [Serializable]
    public class LevelRoots
    {
        [field: SerializeField, BoxGroup("World")] public Transform Environment { get; private set; }
    }
}
