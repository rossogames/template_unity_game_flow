using Rossoforge.Services.Locator;
using Rossoforge.TimeFlow.Service;
using Rossogames.Gameplay.DataBehaviour;
using Rossogames.Progression.Service;
using Rossogames.SceneFlow.Service;
using UnityEngine;

namespace Rossogames.Gameplay.Phases
{
    public class PhaseLevelUnload : GameplayBasePhase
    {
        private ISceneFlowService _sceneFlowService;
        private ITimeFlowService _timeFlowService;
        private IProgressionService _progressionService;

        public PhaseLevelUnload(PhaseLevelUnloadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();
            _timeFlowService = ServiceLocator.Get<ITimeFlowService>();
            _progressionService = ServiceLocator.Get<IProgressionService>();
        }

        public override async void Enter()
        {
            base.Enter();
            await _sceneFlowService.GoToMainScene(onScreenCoveredAsync: OnGameplayUnloading);
        }

        private async Awaitable OnGameplayUnloading()
        {
            _progressionService.SaveProgression();
            await _gameplayService.TransitionToPhaseStandBy();
            await _levelService.UnloadLevel();
            _timeFlowService.ResumeTimeFlow();
            await Resources.UnloadUnusedAssets();
        }
    }
}
