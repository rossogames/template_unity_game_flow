using RossoGames.Buildings.DataEntities;
using RossoGames.LevelObjects.DataAssets;
using UnityEngine;

namespace RossoGames.Buildings.DataAssets
{
    [CreateAssetMenu(fileName = nameof(BuildingDataAsset), menuName = "RossoGames/Data Assets/Building")]
    public class BuildingDataAsset : LevelObjectDataAsset
    {
        [field: SerializeField]
        public BuildingDataEntity BuildingDataEntity { get; set; }
    }
}
