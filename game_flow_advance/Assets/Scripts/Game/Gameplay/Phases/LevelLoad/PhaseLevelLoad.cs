using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Level.Events;
using UnityEngine;

namespace RossoGames.Gameplay.Phases.LevelLoad
{
    public class PhaseLevelLoad : GameplayBasePhase
    {
        public PhaseLevelLoad(PhaseLevelLoadDataBehaviour dataBehaviour) : base(dataBehaviour)
        {
        }

        public override void OnEventInvoked(LevelLoadedEvent eventArg)
        {
            base.OnEventInvoked(eventArg);
            SetInitialCurrencyAmount();

            Debug.LogWarning($"PhaseLevelLoad: Level {eventArg.LevelDataAsset.name} loaded");
        }

        private void SetInitialCurrencyAmount()
        {
            var initialCurrencyAmount = _gameplayService.ServiceData.RunSetupDataEntity.CurrencyAmount;
            _currencyService.ResetCurrency(initialCurrencyAmount.CurrencyDataEntity);
            _currencyService.AddCurrency(initialCurrencyAmount);
        }
    }
}
