using Rossoforge.Services.Locator;
using UnityEngine;

namespace Rossogames.Common.ServiceUpdater
{
    public class ServiceUpdater : MonoBehaviour
    {
        private void Update()
        {
            ServiceLocator.Update();
        }
    }
}
