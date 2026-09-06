using RossoGames.LevelObjects.Components;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerClickables : LevelHandlerBase
    {
        public LevelHandlerClickables(LevelDataService serviceData) : base(serviceData)
        {
        }

        public bool TryClickObject(LayerMask layerMask)
        {
            if (!TryGetClosestPointerHit(layerMask, out Clickable hitComponent))
                return false;

            hitComponent.Click();
            return true;
        }
        public bool TryGetClosestHit(LayerMask layerMask, out Clickable hitComponent)
        {
            return TryGetClosestPointerHit(layerMask, out hitComponent);
        }
    }
}
