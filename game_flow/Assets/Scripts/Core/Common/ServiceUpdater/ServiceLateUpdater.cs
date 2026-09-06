using Rossoforge.Services.Locator;
using UnityEngine;

namespace Rossogames.Common.ServiceUpdater
{
    public class ServiceLateUpdater : MonoBehaviour
    {
        private void LateUpdate()
        {
            ServiceLocator.LateUpdate();
        }
    }
}
