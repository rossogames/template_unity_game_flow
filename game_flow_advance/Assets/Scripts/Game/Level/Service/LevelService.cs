using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Common;
using Rossogames.ContextActions.DataEntities;
using Rossogames.Level.DataContext;
using Rossogames.Level.Events;
using Rossogames.LevelObjects.Components;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public class LevelService : ILevelService, IInitializable
    {
        private IEventService _eventService;
        private IPoolService _poolService;

        private LevelHandlerRooms _levelHandlerRooms;
        private LevelHandlerInteractables _levelHandlerInteractables;

        public LevelDataService _dataService { get; private set; }

        public LevelService(LevelDataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        // -- LEVEL --
        public void LoadLevel(LevelRootsDataContext levelRoots)
        {
            _levelHandlerRooms = new LevelHandlerRooms(_dataService, levelRoots.Rooms);
            _levelHandlerInteractables = new LevelHandlerInteractables(_dataService, levelRoots.ContextActions);

            _levelHandlerRooms.Initialize();
            _levelHandlerInteractables.Initialize();

            _eventService.Raise(new LevelLoadedEvent(_dataService.LevelDataAsset));
        }
        public async Awaitable UnloadLevel()
        {
            _levelHandlerRooms?.Dispose();
            _levelHandlerInteractables?.Dispose();

            _poolService.ForceReturnAll();
            await Awaitable.NextFrameAsync();

            _poolService.Clear(PoolCategories.Gameplay);
        }

        // -- INTERACTABLES --
        public void TryShowContextActions(Interactable interactable)
        {
            _levelHandlerInteractables.TryShowContextActions(interactable);
        }
        public void HideContextActions()
        {
            _levelHandlerInteractables.HideContextActions();
        }
        public bool IsContextActionAvailable(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            return _levelHandlerInteractables.IsAvailable(contextActionDataEntity, interactable);
        }
        public Awaitable InvokeContextAction(ContextActionDataEntity contextActionDataEntity, Interactable interactable)
        {
            return _levelHandlerInteractables.InvokeAction(contextActionDataEntity, interactable);
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            _levelHandlerRooms.OnDrawGizmos();
        }
#endif
    }
}