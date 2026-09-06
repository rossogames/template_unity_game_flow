using Rossoforge.Services.Service;
using Rossogames.ContextActions.DataEntities;
using Rossogames.Level.DataContext;
using Rossogames.LevelObjects.Components;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public interface ILevelService : IService
    {
        LevelDataService _dataService { get; }

        // -- LEVELS --
        void LoadLevel(LevelRootsDataContext levelRoots);
        Awaitable UnloadLevel();

        // -- INTERACTABLES --
        void TryShowContextActions(Interactable interactable);
        void HideContextActions();
        bool IsContextActionAvailable(ContextActionDataEntity contextActionDataEntity, Interactable interactable);
        Awaitable InvokeContextAction(ContextActionDataEntity contextActionDataEntity, Interactable interactable);

#if UNITY_EDITOR
        void OnDrawGizmos();
#endif
    }
}