using Rossoforge.Core.DataStructures;
using RossoGames.Tiles.DataAssets;
using System;
using UnityEngine;

namespace RossoGames.Level.DataAssets
{
    [CreateAssetMenu(fileName = nameof(LevelDataAsset), menuName = "RossoGames/Data Assets/Level")]
    public class LevelDataAsset : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(3, 12)]
        public float CameraZoom { get; private set; }

        [field: SerializeField]
        public Size2<int> Size { get; private set; }

        [field: SerializeField]
        public GameObject EnvironmentAssetReference { get; private set; }

        [field: SerializeField]
        public WeightedList<TileDataAsset> BuildableTiles { get; set; }

        [field: SerializeField]
        public WeightedList<TileDataAsset> TransportTiles { get; set; }
    }
}
