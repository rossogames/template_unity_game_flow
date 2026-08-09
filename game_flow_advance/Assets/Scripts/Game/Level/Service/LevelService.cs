using Rossoforge.Core.Events;
using Rossoforge.Core.Pool;
using Rossoforge.Core.Services;
using Rossoforge.Services;
using RossoGames.Cameras.Service;
using RossoGames.Level.DataTypes;
using RossoGames.Level.Events;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelService : ILevelService, IInitializable
    {
        private IEventService _eventService;
        private ICameraService _cameraService;
        private IPoolService _poolService;

        private LevelHandlerEnvironment _levelHandlerEnvironment;
        
        public LevelServiceData ServiceData { get; private set; }

        public LevelService(LevelServiceData serviceData)
        {
            ServiceData = serviceData;
        }
        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _cameraService = ServiceLocator.Get<ICameraService>();
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        public void LoadLevel(LevelRoots levelRoots)
        {
            _levelHandlerEnvironment = new LevelHandlerEnvironment(ServiceData, levelRoots.Environment);

            _levelHandlerEnvironment.Initialize();

            _cameraService.SetPosition(Vector3.zero); // TODO: set camera position

            _eventService.Raise(new LevelLoadedEvent(ServiceData.CurrentLevelDataAsset));
        }
        public async Awaitable UnloadLevel()
        {
            _levelHandlerEnvironment?.Dispose();

            _poolService.ForceReturnAll();
            await Awaitable.NextFrameAsync();

            _eventService.Raise(new LevelUnloadedEvent(ServiceData.CurrentLevelDataAsset));
        }
    }
}