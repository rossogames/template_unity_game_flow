using Rossoforge.Core.DataStructures;
using RossoGames.Common.Enums;
using UnityEngine;

namespace RossoGames.Tiles.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(TileDataBehaviour), menuName = "RossoGames/Data Behaviour/Tiles/Buildable")]
    public class TileBuildableDataBehaviour : TileDataBehaviour
    {
        [field: SerializeField]
        public WeightedList<Rarities> BuildingRarities { get; set; }
    }
}
