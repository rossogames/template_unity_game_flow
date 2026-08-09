using Rossoforge.Core.Events;
using Rossoforge.Core.Pool;
using Rossoforge.Core.Services;
using Rossoforge.Services;
using RossoGames.Buildings.DataEntities;
using RossoGames.Cameras.Service;
using RossoGames.Level.DataTypes;
using RossoGames.Level.Events;
using RossoGames.Tiles.Components;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelService : ILevelService, IInitializable
    {
        private IEventService _eventService;
        private ICameraService _cameraService;
        private IPoolService _poolService;

        private LevelHandlerTiles _levelHandlerTiles;
        private LevelHandlerEnvironment _levelHandlerEnvironment;
        private LevelHandlerTokens _levelHandlerTokens;
        private LevelHandlerBuildings _levelHandlerBuildings;

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
            _levelHandlerTiles = new LevelHandlerTiles(ServiceData, levelRoots.Tiles);
            _levelHandlerEnvironment = new LevelHandlerEnvironment(ServiceData, levelRoots.Environment);
            _levelHandlerTokens = new LevelHandlerTokens(ServiceData, levelRoots.Tokens);
            _levelHandlerBuildings = new LevelHandlerBuildings(ServiceData, levelRoots.Cards, levelRoots.Buildings);

            _levelHandlerTiles.Initialize();
            _levelHandlerEnvironment.Initialize();
            _levelHandlerTokens.Initialize();
            _levelHandlerBuildings.Initialize();

            _cameraService.SetZoom(ServiceData.CurrentLevelDataAsset.CameraZoom);
            //_cameraService.SetBounds(ServiceData.CurrentLevelDataAsset.Size);

            _eventService.Raise(new LevelLoadedEvent(ServiceData.CurrentLevelDataAsset));
        }
        public async Awaitable UnloadLevel()
        {
            _levelHandlerTiles?.Dispose();
            _levelHandlerEnvironment?.Dispose();
            _levelHandlerTokens?.Dispose();
            _levelHandlerBuildings?.Dispose();

            _poolService.ForceReturnAll();
            await Awaitable.NextFrameAsync();

            _eventService.Raise(new LevelUnloadedEvent(ServiceData.CurrentLevelDataAsset));
        }

        //--TILES--
        public Vector3 GetTokenAnchorPosition(int index)
        {
            return _levelHandlerTiles.GetTokenAnchorPosition(index);
        }
        public int GetTilesCount()
        {
            return _levelHandlerTiles.GetTilesCount();
        }
        public bool IsTileBuildable(int tileIndex)
        {
            return _levelHandlerTiles.IsTileBuildable(tileIndex);
        }
        public bool IsTileTransport(int tileIndex)
        {
            return _levelHandlerTiles.IsTileTransport(tileIndex);
        }
        public Tile GetTile(int tileIndex)
        {
            return _levelHandlerTiles.GetTile(tileIndex);
        }

        //--TOKENS--
        public Awaitable MoveToken(int steps)
        {
            return _levelHandlerTokens.MoveToken(steps);
        }
        public int GeTokenTileIndex()
        {
            return _levelHandlerTokens.GeTokenTileIndex();
        }

        //--BUILDINGS--
        public void ShowCards(int amount)
        {
            _levelHandlerBuildings.ShowCards(amount);
        }
        public void HideCards()
        {
            _levelHandlerBuildings.HideCards();
        }
        public void InstanceBuilding(BuildingDataEntity buildingDataEntity)
        { 
            _levelHandlerBuildings.InstanceBuilding(buildingDataEntity);
        }
    }
}