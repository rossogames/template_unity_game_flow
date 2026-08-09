using RossoGames.Gameplay.DataBehaviour;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.TransportSelection
{
    public class PhaseTransportSelection : GameplayBasePhase
    {
        public PhaseTransportSelection(PhaseTransportSelectionDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _ = EndPhase();
        }

        private async Awaitable EndPhase()
        {
            await Awaitable.WaitForSecondsAsync(1);
            await _gameplayService.TransitionToPhaseDiceRoll();
        }
    }
}
