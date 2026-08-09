using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;
using RossoGames.Level.Events;

namespace RossoGames.Gameplay.Phases.LevelUnload
{
    public class PhaseLevelUnload : GameplayBasePhase
    {
        public PhaseLevelUnload(PhaseLevelUnloadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void OnEventInvoked(GameplayUnloadSceneActivedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _levelService.UnloadLevel();
        }

        public override void OnEventInvoked(LevelUnloadedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);

            // at this point, the level is unloaded, so we can finish the gameplay
            _gameplayService.FinishGameplay();
        }

        public override void OnEventInvoked(GameplayFinishEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _ = _gameplayService.TransitionToPhaseStandBy();
        }
    }
}
