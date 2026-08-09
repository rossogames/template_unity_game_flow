using Rossoforge.Core.DataStructures;
using RossoGames.Common.Enums;
using RossoGames.Tiles.DataBehaviour;
using UnityEngine;

namespace RossoGames.Tiles.Components
{
    public class TileBuildable : Tile
    {
        [field: SerializeField] public Transform BuildingAnchor { get; private set; }

        new public TileBuildableDataBehaviour DataBehaviour => (TileBuildableDataBehaviour)base.DataBehaviour;
        public WeightedList<Rarities> BuildingRarities { get; set; }

        public override void Initialize(TileDataBehaviour dataBehaviour)
        {
            base.Initialize(dataBehaviour);
            if (dataBehaviour is not TileBuildableDataBehaviour)
            {
                Debug.LogError($"TileBuildable can only be initialized with TileBuildableDataBehaviour, but got {dataBehaviour.GetType().Name}");
                return;
            }

            BuildingRarities = DataBehaviour.BuildingRarities;
        }
    }
}
