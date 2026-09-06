using UnityEngine;

namespace RossoGames.Common.ServiceUpdater
{
    public class ServiceFixedUpdater : MonoBehaviour
    {
        private void FixedUpdate()
        {
            ServiceLocator.FixedUpdate();
        }
    }
}
