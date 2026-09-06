using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Inputs.Service;
using UnityEngine;

namespace Rossogames.Cameras.Service
{
    public class CameraService : ICameraService, IInitializable
    {
        private IInputsService _inputsService;

        private Camera _camera;

        private CameraDataService _dataService;

        public Camera Camera => _camera;

        public CameraService(CameraDataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _inputsService = ServiceLocator.Get<IInputsService>();
        }

        public void SetupCamera(Camera camera)
        {
            _camera = camera;
        }
    }
}