using UnityEngine;

namespace RossoGames.Common
{
    public static class GameLayers
    {
        public static readonly int UILayer = LayerMask.NameToLayer("UI");
        public static readonly int TileLayer = LayerMask.NameToLayer("Tile");

        public static readonly LayerMask UIMask = 1 << UILayer;
        public static readonly LayerMask TileMask = 1 << TileLayer;
    }
}
