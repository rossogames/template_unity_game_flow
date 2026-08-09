using Rossoforge.Core.Services;
using RossoGames.Level.DataTypes;
using UnityEngine;

namespace RossoGames.Level.Service
{
    public interface ILevelService : IService
    {
        LevelServiceData ServiceData { get; }

        void LoadLevel(LevelRoots levelRoots);
        Awaitable UnloadLevel();
    }
}