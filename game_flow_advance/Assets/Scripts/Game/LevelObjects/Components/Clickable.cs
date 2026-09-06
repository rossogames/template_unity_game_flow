using RossoGames.LevelObjects.Events;
using UnityEngine;

namespace RossoGames.LevelObjects.Components
{
    [RequireComponent(typeof(LevelObject))]
    public class Clickable : MonoBehaviour
    {
        private IEventService _eventService;

        private ILevelObjectClickable _clickable;
        private LevelObject _levelObject;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            TryGetComponent(out _clickable);
            _levelObject = GetComponent<LevelObject>();
        }

        public void Click()
        {
            _clickable?.OnClick();
            _eventService.Raise(new LevelObjectClickedEvent(_levelObject));
        }
    }
}
