using Rossoforge.Utils.StateMachine;
using Rossogames.Gameplay.Phases;
using UnityEngine;

namespace Rossogames.Gameplay.Service
{
    public class GameplayStateMachine : StateMachine<GameplayBasePhase>
    {
        public PhaseStandBy PhaseStandBy { get; private set; }
        public PhaseLevelLoad PhaseLevelLoad { get; private set; }
        public PhaseLevelUnload PhaseLevelUnload { get; private set; }
        public PhaseExploration PhaseExploration { get; private set; }

        public GameplayStateMachine(GameplayDataService gameplayServiceData)
        {
            PhaseStandBy = new PhaseStandBy(gameplayServiceData.StandByDataBehaviour);
            PhaseLevelLoad = new PhaseLevelLoad(gameplayServiceData.LevelLoadDataBehaviour);
            PhaseLevelUnload = new PhaseLevelUnload(gameplayServiceData.LevelUnloadDataBehaviour);
            PhaseExploration = new PhaseExploration(gameplayServiceData.ExplorationDataBehaviour);
        }

        public override async Awaitable<bool> TransitionTo(GameplayBasePhase nextState)
        {
            if (!await base.TransitionTo(nextState))
                return false;

            return true;
        }
    }
}
