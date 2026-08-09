using Rossoforge.Core.Services;
using RossoGames.Buildings.DataEntities;
using RossoGames.Level.DataTypes;
using RossoGames.Tiles.Components;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public interface ILevelService : IService
    {
        LevelServiceData ServiceData { get; }

        void LoadLevel(LevelRoots levelRoots);
        Awaitable UnloadLevel();

        //--TILES--
        Vector3 GetTokenAnchorPosition(int index);
        int GetTilesCount();
        bool IsTileBuildable(int tileIndex);
        bool IsTileTransport(int tileIndex);
        Tile GetTile(int tileIndex);

        //--TOKENS--
        Awaitable MoveToken(int steps);
        int GeTokenTileIndex();

        //--BUILDINGS--
        void ShowCards(int amount);
        void HideCards();
        void InstanceBuilding(BuildingDataEntity buildingDataEntity);
    }
}