using Rossoforge.Services.Service;
using RossoGames.Level.DataTypes;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public interface ILevelService : IService
    {
        LevelDataService ServiceData { get; }

        void LoadLevel(LevelRoots levelRoots);
        Awaitable UnloadLevel();
    }
}