using Rossoforge.Services.Locator;
using UnityEngine;

namespace Rossogames.Common.ServiceUpdater
{
    public class ServiceFixedUpdater : MonoBehaviour
    {
        private void FixedUpdate()
        {
            ServiceLocator.FixedUpdate();
        }
    }
}
