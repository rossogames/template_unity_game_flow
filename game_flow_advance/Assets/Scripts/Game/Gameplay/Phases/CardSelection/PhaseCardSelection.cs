using RossoGames.Cards.Events;
using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;

namespace RossoGames.Gameplay.Phases.CardSelection
{
    public class PhaseCardSelection : GameplayBasePhase
    {
        new public PhaseCardSelectionDataBehaviour DataBehaviour => (PhaseCardSelectionDataBehaviour)base.DataBehaviour;

        public PhaseCardSelection(PhaseCardSelectionDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _levelService.ShowCards(DataBehaviour.CardsAmount);
        }

        public override void OnEventInvoked(BuildingCardClickedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            _currencyService.SpendCurrency(eventArg.DataEntity.Cost);

            _gameplayService.InitializePhaseBuilding(eventArg.DataEntity);

            _levelService.HideCards();
            _gameplayService.TransitionToPhasePhaseBuilding();
        }

        public override void OnEventInvoked(GameplayCardSelectionSkippedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);

            _levelService.HideCards();
            _gameplayService.TransitionToPhaseDiceRoll();
        }
    }
}
