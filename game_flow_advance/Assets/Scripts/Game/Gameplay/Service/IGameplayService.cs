using Rossoforge.Core.Services;
using System;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public interface IGameplayService : IService, IInitializable
    {
        GameplayServiceData ServiceData { get; }

        Type GetCurrentPhase();
        Awaitable<bool> TransitionToPhaseStandBy();

        void StartGameplay();
    }
}