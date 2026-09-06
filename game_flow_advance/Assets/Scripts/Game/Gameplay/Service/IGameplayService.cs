using Rossoforge.Services.Service;
using System;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public interface IGameplayService : IService, IInitializable
    {
        GameplayDataService ServiceData { get; }

        Type GetCurrentPhase();
        Awaitable<bool> TransitionToPhaseStandBy();

        void StartGameplay();
    }
}