using Rossoforge.Core.Services;
using UnityEngine;

namespace RossoGames.Raycast.Service
{
    public interface IRaycastService : IService
    {
        bool TryGetClosestPointerHit(LayerMask layerMask, out RaycastHit hit);
        bool IsPointerOverUIElement();
    }
}
