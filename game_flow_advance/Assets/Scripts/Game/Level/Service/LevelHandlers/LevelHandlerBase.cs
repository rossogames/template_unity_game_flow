using Rossoforge.Core.Events;
using Rossoforge.Core.Pool;
using Rossoforge.Services;
using RossoGames.Raycast.Service;
using System;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public abstract class LevelHandlerBase : IDisposable
    {
        private readonly IRaycastService _raycastService;
        protected readonly IEventService _eventService;
        protected readonly ILevelService _levelService;
        protected readonly IPoolService _poolService;

        protected LevelServiceData _serviceData { get; private set; }

        public LevelHandlerBase(LevelServiceData serviceData)
        {
            _serviceData = serviceData;

            _eventService = ServiceLocator.Get<IEventService>();
            _levelService = ServiceLocator.Get<ILevelService>();
            _raycastService = ServiceLocator.Get<IRaycastService>();
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
        }

#if UNITY_EDITOR
        public virtual void OnDrawGizmos()
        {
        }
#endif

        protected bool TryGetClosestPointerHit<T>(LayerMask layerMask, out T hitComponent)
        {
            hitComponent = default;
            if (_raycastService.TryGetClosestPointerHit(layerMask, out RaycastHit hit))
            {
                hitComponent = GetHitComponent<T>(hit);
                return hitComponent != null;
            }

            return false;
        }
        private T GetHitComponent<T>(RaycastHit? hitInfo)
        {
            if (hitInfo.Value.transform.gameObject.TryGetComponent(out T obj))
                return obj;

            return default;
        }
    }
}
