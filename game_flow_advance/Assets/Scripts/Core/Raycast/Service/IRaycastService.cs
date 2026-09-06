using Rossoforge.Services.Service;
using UnityEngine;

namespace Rossogames.Raycast.Service
{
    public interface IRaycastService : IService
    {
        bool TryGetClosestPointerHit(LayerMask layerMask, out RaycastHit hit);
        bool IsPointerOverUIElement();
    }
}
