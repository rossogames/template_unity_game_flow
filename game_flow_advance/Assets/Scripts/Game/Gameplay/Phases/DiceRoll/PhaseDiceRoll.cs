using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.DiceRoll
{
    public class PhaseDiceRoll : GameplayBasePhase
    {
        new public PhaseDiceRollDataBehaviour DataBehaviour => (PhaseDiceRollDataBehaviour)base.DataBehaviour;

        public PhaseDiceRoll(PhaseDiceRollDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void OnEventInvoked(GameplayRollDiceStartedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _ = RollDices();
        }

        private async Awaitable RollDices()
        {
            await _gameplayService.TransitionToPhaseTokenMovement();

            var value = Random.Range(DataBehaviour.DiceValueRange.Min, DataBehaviour.DiceValueRange.Max + 1);
            _eventService.Raise(new GameplayRollDiceEndedEvent(value));
        }
    }
}
