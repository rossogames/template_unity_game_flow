using RossoGames.LevelObjects.Components;
using RossoGames.Tiles.DataBehaviour;
using UnityEngine;

namespace RossoGames.Tiles.Components
{
    public abstract class Tile : LevelObject
    {
        [field: SerializeField] public Transform TokenAnchor { get; private set; }

        public TileDataBehaviour DataBehaviour { get; private set; }

        public virtual void Initialize(TileDataBehaviour dataBehaviour)
        {
            DataBehaviour = dataBehaviour;
        }
    }
}
