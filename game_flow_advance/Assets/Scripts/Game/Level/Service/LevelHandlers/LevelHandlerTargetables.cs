using RossoGames.LevelObjects.Components;
using RossoGames.LevelObjects.Events;
using System;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerTargetables : LevelHandlerBase
    {
        private Targetable _currentTarget;

        public LevelHandlerTargetables(LevelDataService serviceData) : base(serviceData)
        {
        }
        public override void Dispose()
        {
            base.Dispose();
            _currentTarget = null;
        }

        public bool TryTargetObject(LayerMask layerMask, Predicate<Targetable> customCondition)
        {
            TryGetClosestPointerHit(layerMask, out Targetable newTarget);

            if (newTarget == _currentTarget)
                return true;

            if (newTarget != null && customCondition != null && !customCondition(newTarget))
                return false;

            _eventService.Raise<LevelObjectUntargetedEvent>();

            _currentTarget = newTarget;

            if (newTarget != null)
                newTarget.TryTarget();
            else
                _eventService.Raise(new LevelObjectTargetedEvent(null));

            return newTarget != null;
        }
        public bool TryGetClosestHit(LayerMask layerMask, out Targetable hitComponent)
        {
            return TryGetClosestPointerHit<Targetable>(layerMask, out hitComponent);
        }
        public void UntargetObjects()
        {
            if (_currentTarget == null)
                return;

            _currentTarget = null;
            _eventService.Raise<LevelObjectUntargetedEvent>();
        }
        public T GetCurrentTarget<T>() where T : LevelObject
        {
            if (_currentTarget == null)
                return null;

            var levelObject = _currentTarget.GetTargetedLevelObject();
            if (levelObject == null)
                return null;

            return levelObject is T typedLevelObject ? typedLevelObject : null;
        }
    }
}
