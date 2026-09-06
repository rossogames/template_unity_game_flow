using RossoGames.Cameras.Service;
using UnityEngine;

namespace RossoGames.Cameras.Components
{
    public class CameraSetter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private ICameraService _cameraService;

        private void Awake()
        {
            _cameraService = ServiceLocator.Get<ICameraService>();
            _cameraService.SetupCamera(_camera, transform);
        }
    }
}
