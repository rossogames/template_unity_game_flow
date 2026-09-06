using UnityEngine;

namespace Rossogames.Common
{
    public static class GameLayers
    {
        public static readonly int UILayer = LayerMask.NameToLayer("UI");

        public static readonly LayerMask UIMask = 1 << UILayer;
    }
}
