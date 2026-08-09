using RossoGames.LevelObjects.DataAssets;
using RossoGames.Tiles.DataBehaviour;
using UnityEngine;

namespace RossoGames.Tiles.DataAssets
{
    [CreateAssetMenu(fileName = nameof(TileDataAsset), menuName = "RossoGames/Data Assets/Tile")]
    public class TileDataAsset : LevelObjectDataAsset
    {
        [field: SerializeField]
        public TileDataBehaviour DataBehaviour { get; private set; }
    }
}
