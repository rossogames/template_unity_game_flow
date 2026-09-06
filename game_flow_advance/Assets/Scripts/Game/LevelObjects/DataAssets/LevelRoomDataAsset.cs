using Rossoforge.Pool.DataConfig;
using UnityEngine;

namespace Rossogames.LevelObjects.DataAssets
{
    [CreateAssetMenu(fileName = nameof(LevelRoomDataAsset), menuName = "Rossogames/Data Assets/Level Room")]
    public class LevelRoomDataAsset : ScriptableObject
    {
        [field: SerializeField]
        public PooledGameobjectDataConfig AssetReference { get; private set; }

        [field: SerializeField]
        public Vector3 Position { get; private set; }

        [field: SerializeField]
        public Vector3 Rotation { get; private set; }

        [field: SerializeField]
        public LevelRoomDataAsset[] NextRoom { get; private set; }
    }
}
