using RossoGames.LevelObjects.DataBehaviour;
using RossoGames.LevelObjects.Events;
using UnityEngine;

namespace RossoGames.LevelObjects.Components
{
    [RequireComponent(typeof(LevelObject))]
    public class Targetable : MonoBehaviour,
        IEventListener<LevelObjectUntargetedEvent>
    {
        private IEventService _eventService;

        private Highlightable _highlightable;
        private ILevelObjectTargetEligibility _targetEligibility;
        private ILevelObjectTargetEnter _targetEnter;
        private ILevelObjectTargetExit _targetExit;
        private LevelObject _levelObject;

        [field: SerializeField]
        public OutlineDataBehaviour OutlineDataBehaviour { get; set; }

        public bool IsTargeted { get; private set; }

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            TryGetComponent(out _highlightable);
            TryGetComponent(out _targetEligibility);
            TryGetComponent(out _targetEnter);
            TryGetComponent(out _targetExit);
            _levelObject = GetComponent<LevelObject>();
        }

        public void TryTarget()
        {
            if (!IsTargetEligible())
                return;

            IsTargeted = true;
            _targetEnter?.OnTargeted();

            if (_highlightable != null && OutlineDataBehaviour != null)
                _highlightable.ShowHighlight(OutlineDataBehaviour);

            _eventService.Raise(new LevelObjectTargetedEvent(_levelObject));
            _eventService.RegisterListener<LevelObjectUntargetedEvent>(this);
        }

        public void RefreshOutline()
        {
            if (!IsTargeted)
                return;

            if (OutlineDataBehaviour == null)
                return;

            if (_highlightable != null)
                _highlightable.ShowHighlight(OutlineDataBehaviour);
        }

        public void OnEventInvoked(LevelObjectUntargetedEvent eventArg)
        {
            if (!IsTargeted)
                return;

            if (_highlightable != null)
                _highlightable.HideHighlight();

            IsTargeted = false;
            _targetExit?.OnUntargeted();

            _eventService.UnregisterListener<LevelObjectUntargetedEvent>(this);
        }

        public LevelObject GetTargetedLevelObject()
        {
            return _levelObject;
        }

        private bool IsTargetEligible()
        {
            return _targetEligibility == null || _targetEligibility.IsTargetEligible();
        }
    }
}
