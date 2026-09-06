using Rossoforge.Services.Service;
using Rossogames.Level.DataContext;
using UnityEngine;

namespace Rossogames.Level.Service
{
    public interface ILevelService : IService
    {
        LevelDataService _dataService { get; }

        // -- LEVELS --
        void LoadLevel(LevelRootsDataContext levelRoots);
        Awaitable UnloadLevel();

#if UNITY_EDITOR
        void OnDrawGizmos();
#endif
    }
}