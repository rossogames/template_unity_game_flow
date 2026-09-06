using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Cameras.Service;
using Rossogames.Inputs.Service;
using System;
using UnityEngine;

namespace Rossogames.Raycast.Service
{
    public class RaycastService : IRaycastService, IInitializable
    {
        private ICameraService _cameraService;
        private IInputsService _inputsService;

        private Vector2 _lastScreenPosition;

        public void Initialize()
        {
            _cameraService = ServiceLocator.Get<ICameraService>();
            _inputsService = ServiceLocator.Get<IInputsService>();
        }

        public bool TryGetClosestPointerHit(LayerMask layerMask, out RaycastHit hit)
        {
            hit = default;

            if (_cameraService.Camera == null)
                return false;

            Ray ray = _cameraService.Camera.ScreenPointToRay(_lastScreenPosition);
            return Physics.Raycast(ray, out hit, 100f, layerMask);
        }
        public bool IsPointerOverUIElement()
        {
            throw new NotImplementedException();
            /*
            if (EventSystem.current == null)
                return false;

            var eventData = new PointerEventData(EventSystem.current)
            {
                //position = _inputsService.PointerPosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            var layerMask = GameLayers.UIMask;
            foreach (var result in results)
            {
                if (layerMask.ContainsLayer(result.gameObject.layer))
                    return true;
            }

            return false;
            */
        }
    }
}
