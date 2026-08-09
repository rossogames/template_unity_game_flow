using Rossoforge.Utils.StateMachine;
using RossoGames.Gameplay.Phases;
using RossoGames.Gameplay.Phases.Building;
using RossoGames.Gameplay.Phases.CardSelection;
using RossoGames.Gameplay.Phases.DiceRoll;
using RossoGames.Gameplay.Phases.LevelLoad;
using RossoGames.Gameplay.Phases.LevelUnload;
using RossoGames.Gameplay.Phases.StandBy;
using RossoGames.Gameplay.Phases.TokenMovement;
using RossoGames.Gameplay.Phases.TransportSelection;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public class GameplayStateMachine : StateMachine<GameplayBasePhase>
    {
        public PhaseStandBy PhaseStandBy { get; private set; }
        public PhaseLevelLoad PhaseLevelLoad { get; private set; }
        public PhaseLevelUnload PhaseLevelUnload { get; private set; }
        public PhaseDiceRoll PhaseDices { get; private set; }
        public PhaseTokenMovement PhaseTokenMovement { get; set; }
        public PhaseTransportSelection PhaseTransportSelection { get; set; }
        public PhaseCardSelection PhaseCardSelection { get; set; }
        public PhaseBuilding PhasePhaseBuilding { get; set; }

        public GameplayStateMachine(GameplayServiceData gameplayServiceData)
        {
            PhaseStandBy = new PhaseStandBy(gameplayServiceData.StandByDataBehaviour);
            PhaseLevelLoad = new PhaseLevelLoad(gameplayServiceData.LevelLoadDataBehaviour);
            PhaseLevelUnload = new PhaseLevelUnload(gameplayServiceData.LevelUnloadDataBehaviour);
            PhaseDices = new PhaseDiceRoll(gameplayServiceData.DicesDataBehaviour);
            PhaseTokenMovement = new PhaseTokenMovement(gameplayServiceData.TokenMovementDataBehaviour);
            PhaseTransportSelection = new PhaseTransportSelection(gameplayServiceData.TransportSelectionDataBehaviour);
            PhaseCardSelection = new PhaseCardSelection(gameplayServiceData.CardSelectionDataBehaviour);
            PhasePhaseBuilding = new PhaseBuilding(gameplayServiceData.BuildingDataBehaviour);
        }

        public override async Awaitable<bool> TransitionTo(GameplayBasePhase nextState)
        {
            if (!await base.TransitionTo(nextState))
                return false;

            return true;
        }
    }
}
