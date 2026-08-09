using Rossoforge.Core.Services;
using RossoGames.Buildings.DataAssets;
using RossoGames.Buildings.DataEntities;
using System;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public interface IGameplayService : IService, IInitializable
    {
        GameplayServiceData ServiceData { get; }

        Type GetCurrentPhase();
        Awaitable<bool> TransitionToPhaseStandBy();
        Awaitable<bool> TransitionToPhaseDiceRoll();
        Awaitable<bool> TransitionToPhaseTokenMovement();
        Awaitable<bool> TransitionToPhaseTransportSelection();
        Awaitable<bool> TransitionToPhaseCardSelection();
        Awaitable<bool> TransitionToPhasePhaseBuilding();

        void InitializePhaseBuilding(BuildingDataEntity dataEntity);

        void StartGameplay();
        void FinishGameplay();
    }
}