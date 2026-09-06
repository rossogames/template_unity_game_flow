using Rossogames.Gameplay.DataBehaviour;
using Rossogames.Level.Events;

namespace Rossogames.Gameplay.Phases
{
    public class PhaseLevelLoad : GameplayBasePhase
    {
        public PhaseLevelLoad(PhaseLevelLoadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void OnEventInvoked(LevelLoadedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _gameplayService.TransitionToPhaseExploration();
        }
    }
}
