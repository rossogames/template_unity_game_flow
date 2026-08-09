using Rossoforge.Utils.StateMachine;
using RossoGames.Gameplay.Phases;
using RossoGames.Gameplay.Phases.LevelLoad;
using RossoGames.Gameplay.Phases.LevelUnload;
using RossoGames.Gameplay.Phases.StandBy;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public class GameplayStateMachine : StateMachine<GameplayBasePhase>
    {
        public PhaseStandBy PhaseStandBy { get; private set; }
        public PhaseLevelLoad PhaseLevelLoad { get; private set; }
        public PhaseLevelUnload PhaseLevelUnload { get; private set; }

        public GameplayStateMachine(GameplayServiceData gameplayServiceData)
        {
            PhaseStandBy = new PhaseStandBy(gameplayServiceData.StandByDataBehaviour);
            PhaseLevelLoad = new PhaseLevelLoad(gameplayServiceData.LevelLoadDataBehaviour);
            PhaseLevelUnload = new PhaseLevelUnload(gameplayServiceData.LevelUnloadDataBehaviour);
        }

        public override async Awaitable<bool> TransitionTo(GameplayBasePhase nextState)
        {
            if (!await base.TransitionTo(nextState))
                return false;

            return true;
        }
    }
}
