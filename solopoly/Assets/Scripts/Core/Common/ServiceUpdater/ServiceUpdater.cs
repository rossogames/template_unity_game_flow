using Rossoforge.Services;
using UnityEngine;

namespace RossoGames.Common.ServiceUpdater
{
    public class ServiceUpdater : MonoBehaviour
    {
        private void Update()
        {
            ServiceLocator.Update();
        }
    }
}
