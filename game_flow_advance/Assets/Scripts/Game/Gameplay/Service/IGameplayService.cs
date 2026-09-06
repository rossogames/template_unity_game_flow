using Rossoforge.Services.Service;
using System;
using UnityEngine;

namespace Rossogames.Gameplay.Service
{
    public interface IGameplayService : IService
    {
        GameplayDataService ServiceData { get; }

        Type GetCurrentPhase();
        Awaitable<bool> TransitionToPhaseStandBy();
        Awaitable<bool> TransitionToPhaseExploration();

        void StartGameplay();
    }
}