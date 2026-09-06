using Rossoforge.Services.Locator;
using Rossogames.Cameras.Service;
using UnityEngine;

namespace Rossogames.Cameras.Components
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        private ICameraService _cameraService;

        private void Awake()
        {
            _cameraService = ServiceLocator.Get<ICameraService>();
            _cameraService.SetupCamera(_mainCamera);
        }
    }
}
