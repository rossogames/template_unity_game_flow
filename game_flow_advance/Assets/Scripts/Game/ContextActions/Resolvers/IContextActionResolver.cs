using Rossogames.ContextActions.DataEntities;
using Rossogames.LevelObjects.Components;
using UnityEngine;

namespace Rossogames.ContextActions.Resolver
{
    public interface IContextActionResolver
    {
        void Initialize(ContextActionDataEntity contextActionDataEntity, Interactable interactable);
        bool IsAvailable();
        Awaitable Invoke();
    }
}
