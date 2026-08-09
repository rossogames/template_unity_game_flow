using Rossoforge.Services;
using UnityEngine;

namespace RossoGames.Common.ServiceUpdater
{
    public class ServiceLateUpdater : MonoBehaviour
    {
        private void LateUpdate()
        {
            ServiceLocator.LateUpdate();
        }
    }
}
