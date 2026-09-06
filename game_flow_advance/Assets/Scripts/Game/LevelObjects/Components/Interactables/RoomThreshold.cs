using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossogames.LevelObjects.DataAssets;
using Rossogames.LevelObjects.Events;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    public class RoomThreshold : MonoBehaviour,
        IAutomaticInteractionEnter
    {
        private IEventService _eventService;

        private LevelRoomDataAsset _dataAsset;
        private Collider _collider;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _collider = GetComponent<Collider>();
        }

        public void Initialize(LevelRoomDataAsset dataAsset)
        {
            _dataAsset = dataAsset;
        }

        public void OnAutomaticInteractionEnter()
        {
            _eventService.Raise(new RoomEnterEvent(_dataAsset));
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_collider == null)
                return;

            var boxCollider = (BoxCollider)_collider;

            Matrix4x4 previousMatrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            Gizmos.DrawCube(boxCollider.center, boxCollider.size);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);

            Gizmos.matrix = previousMatrix;
        }
#endif
    }
}
