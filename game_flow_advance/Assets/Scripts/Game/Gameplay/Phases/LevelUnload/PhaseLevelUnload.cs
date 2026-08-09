using Rossoforge.Services;
using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;
using RossoGames.SceneFlow.Service;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.LevelUnload
{
    public class PhaseLevelUnload : GameplayBasePhase
    {
        private ISceneFlowService _sceneFlowService;

        public PhaseLevelUnload(PhaseLevelUnloadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();
        }

        public override async void Enter()
        {
            base.Enter();
            await _sceneFlowService.GoToMainScene(onScreenCoveredAsync: OnGameplayUnloading);
            //_levelService.UnloadLevel();
        }

        public override void OnEventInvoked(GameplayFinishEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _ = _gameplayService.TransitionToPhaseStandBy();
        }

        private async Awaitable OnGameplayUnloading()
        {
            Debug.LogWarning("OnGameplayUnloading");
        }
    }
}
