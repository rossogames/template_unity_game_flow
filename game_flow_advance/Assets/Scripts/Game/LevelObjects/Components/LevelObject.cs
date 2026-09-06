using RossoGames.Level.Service;
using UnityEngine;

namespace RossoGames.LevelObjects.Components
{
    [DisallowMultipleComponent]
    public class LevelObject : MonoBehaviour, ILevelObject
    {
        protected IEventService _eventService;
        protected ILevelService _levelService;

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        protected virtual void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _levelService = ServiceLocator.Get<ILevelService>();
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }
    }
}
