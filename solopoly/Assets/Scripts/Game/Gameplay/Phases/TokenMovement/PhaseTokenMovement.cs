using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;

namespace RossoGames.Gameplay.Phases.TokenMovement
{
    public class PhaseTokenMovement : GameplayBasePhase
    {
        public PhaseTokenMovement(PhaseTokenMovementDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override async void OnEventInvoked(GameplayRollDiceEndedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            await _levelService.MoveToken(eventArg.DiceValue);
        }

        public override void OnEventInvoked(GameplayTokenLandedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);

            if (_levelService.IsTileTransport(eventArg.TileIndex))
            {
                _gameplayService.TransitionToPhaseTransportSelection();
                return;
            }

            if (_levelService.IsTileBuildable(eventArg.TileIndex))
            {
                _gameplayService.TransitionToPhaseCardSelection();
                return;
            }
        }
    }
}
