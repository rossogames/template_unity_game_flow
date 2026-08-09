using Rossoforge.Core.TimeFlow;
using Rossoforge.Services;
using RossoGames.Gameplay.DataBehaviour;
using RossoGames.SceneFlow.Service;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.LevelUnload
{
    public class PhaseLevelUnload : GameplayBasePhase
    {
        private ISceneFlowService _sceneFlowService;
        private ITimeFlowService _timeFlowService;

        public PhaseLevelUnload(PhaseLevelUnloadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();
            _timeFlowService = ServiceLocator.Get<ITimeFlowService>();
        }

        public override async void Enter()
        {
            base.Enter();
            await _sceneFlowService.GoToMainScene(onScreenCoveredAsync: OnGameplayUnloading);
        }

        private async Awaitable OnGameplayUnloading()
        {
            await _gameplayService.TransitionToPhaseStandBy();
            await _levelService.UnloadLevel();
            _timeFlowService.ResumeTimeFlow();
        }
    }
}
