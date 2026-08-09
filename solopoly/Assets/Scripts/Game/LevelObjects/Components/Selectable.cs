using Rossoforge.Core.Events;
using Rossoforge.Services;
using RossoGames.LevelObjects.DataBehaviour;
using RossoGames.LevelObjects.Events;
using UnityEngine;

namespace RossoGames.LevelObjects.Components
{
    [RequireComponent(typeof(LevelObject))]
    public class Selectable : MonoBehaviour,
        IEventListener<LevelObjectUnselectedEvent>
    {
        private IEventService _eventService;

        private Highlightable _highlightable;
        private ILevelObjectSelectionEligibility _selectionEligibility;
        private ILevelObjectSelectionEnter _selectionEnter;
        private ILevelObjectSelectionExit _selectionExit;
        private LevelObject _levelObject;

        [field: SerializeField]
        public OutlineDataBehaviour OutlineDataBehaviour { get; private set; }
        public bool IsSelected { get; private set; }

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            TryGetComponent(out _highlightable);
            TryGetComponent(out _selectionEligibility);
            TryGetComponent(out _selectionEnter);
            TryGetComponent(out _selectionExit);
            _levelObject = GetComponent<LevelObject>();
        }

        public void TrySelect()
        {
            if (!IsSelectionEligible())
                return;

            IsSelected = true;
            _selectionEnter?.OnSelected();

            if (_highlightable != null)
                _highlightable.ShowHighlight(OutlineDataBehaviour);

            _eventService.Raise(new LevelObjectSelectedEvent(_levelObject));
            _eventService.RegisterListener<LevelObjectUnselectedEvent>(this);
        }

        public LevelObject GetSelectedLevelObject()
        {
            return _levelObject;
        }

        public void OnEventInvoked(LevelObjectUnselectedEvent eventArg)
        {
            if (!IsSelected)
                return;

            if (_highlightable != null)
                _highlightable.HideHighlight();

            IsSelected = false;
            _selectionExit?.OnUnselected();

            _eventService.UnregisterListener<LevelObjectUnselectedEvent>(this);
        }

        private bool IsSelectionEligible()
        {
            return _selectionEligibility == null || _selectionEligibility.IsSelectableEligible();
        }
    }
}
