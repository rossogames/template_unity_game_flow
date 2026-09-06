using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Level.DataContext;
using Rossogames.Level.Events;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public class LevelService : ILevelService, IInitializable
    {
        private IEventService _eventService;
        private IPoolService _poolService;

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
            // TODO: Load level data here

            _eventService.Raise(new LevelLoadedEvent(_dataService.LevelDataAsset));
        }
        public async Awaitable UnloadLevel()
        {
            _poolService.ForceReturnAll();
            await Awaitable.NextFrameAsync();
        }


#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            // OPTIONAL: Draw level gizmos here
        }
#endif
    }
}