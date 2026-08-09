using Rossoforge.Core.Events;
using Rossoforge.Core.Services;
using Rossoforge.Extensions;
using Rossoforge.Services;
using RossoGames.Cameras.Service;
using RossoGames.Common;
using RossoGames.Inputs.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RossoGames.Raycast.Service
{
    public class RaycastService : IRaycastService, IInitializable, IDisposable,
        IEventListener<CursorMovedEvent>
    {
        private IEventService _eventService;
        private ICameraService _cameraService;

        private Vector2 _lastScreenPosition;

        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _cameraService = ServiceLocator.Get<ICameraService>();

            _eventService.RegisterListener<CursorMovedEvent>(this);
        }
        public void Dispose()
        {
            _eventService.UnregisterListener<CursorMovedEvent>(this);
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
            if (EventSystem.current == null)
                return false;

            var eventData = new PointerEventData(EventSystem.current)
            {
                position = _lastScreenPosition
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
        }

        public void OnEventInvoked(CursorMovedEvent eventArg)
        {
            _lastScreenPosition = eventArg.ScreenPosition;
        }
    }
}
