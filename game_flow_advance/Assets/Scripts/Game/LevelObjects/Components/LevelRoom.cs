using Rossoforge.Pool.Service;
using Rossogames.LevelObjects.DataAssets;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    public class LevelRoom : MonoBehaviour
    {
        private IPooledObject _pooledObject;
        private RoomThreshold[] _triggers;

        public LevelRoomDataAsset DataAsset { get; private set; }

        public void Initialize(LevelRoomDataAsset dataAsset)
        {
            DataAsset = dataAsset;
            foreach (var trigger in _triggers)
                trigger.Initialize(DataAsset);

            _pooledObject ??= GetComponent<IPooledObject>();
        }

        private void Awake()
        {
            _triggers = GetComponentsInChildren<RoomThreshold>();
        }

        public void ReturnToPool()
        {
            _pooledObject?.ReturnToPool();
        }
    }
}
