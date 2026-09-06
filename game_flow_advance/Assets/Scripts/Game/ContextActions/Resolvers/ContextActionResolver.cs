using Rossogames.ContextActions.DataEntities;
using Rossogames.LevelObjects.Components;
using UnityEngine;

namespace Rossogames.ContextActions.Resolver
{
    public class ContextActionResolver<T> : IContextActionResolver
        where T : ContextActionDataEntity
    {
        private Interactable _interactable;
        private T _contextActionDataEntity;
        private IContextActionListener<T> _listener;

        public void Initialize(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            _contextActionDataEntity = (T)contextActionDataEntity;
            _interactable = interactable;

            _listener = _interactable.GetComponent<IContextActionListener<T>>();
        }
        public bool IsAvailable()
        {
            return _listener.IsContextActionEnabled(_contextActionDataEntity);
        }
        public async Awaitable Invoke()
        {
            await _listener.OnContextActionInvoke(_contextActionDataEntity);
        }
    }
}
