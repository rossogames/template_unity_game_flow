using UnityEngine;

namespace RossoGames.Buildings.DataAssets
{
    [CreateAssetMenu(fileName = nameof(BuildingCollectionDataAsset), menuName = "RossoGames/Data Assets/BuildingCollection")]
    public class BuildingCollectionDataAsset : ScriptableObject
    {
        [field: SerializeField]
        public BuildingDataAsset[] BuildingDataAssets { get; set; }
    }
}
