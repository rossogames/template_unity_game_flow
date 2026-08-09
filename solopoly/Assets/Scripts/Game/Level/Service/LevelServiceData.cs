using Rossoforge.Pool.Data;
using RossoGames.Buildings.DataAssets;
using RossoGames.Level.DataAssets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Level.Service
{
    [CreateAssetMenu(fileName = nameof(LevelServiceData), menuName = "RossoGames/Service Data/Level")]
    public class LevelServiceData : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Building Collection")]
        public BuildingCollectionDataAsset BuildingCollectionDataAsset { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Current Level")]
        public LevelDataAsset CurrentLevelDataAsset { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Asset References")]
        [field: LabelText("Player Token")]
        public GameObject PlayerTokenAssetReference { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Asset References")]
        [field: LabelText("Building Card")]
        public PooledGameobjectData BuildingCardAssetReference { get; private set; }
    }
}