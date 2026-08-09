using Rossoforge.Core.Events;
using Rossoforge.Services;
using RossoGames.Currencies.DataEntities;
using RossoGames.Currencies.DataTypes;
using RossoGames.Currencies.Events;
using System.Collections.Generic;

namespace RossoGames.Currencies.Service
{
    public class CurrencyService : ICurrencyService
    {
        private IEventService _eventService;

        private Dictionary<string, int> _currencyAmount;

        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            LoadCurrencyAmounts();
        }

        public CurrencyAmount GetCurrencyAmount(CurrencyDataEntity currencyDataEntity)
        {
            if (!_currencyAmount.ContainsKey(currencyDataEntity.name))
                return new CurrencyAmount(currencyDataEntity, 0);

            return new CurrencyAmount(currencyDataEntity, _currencyAmount[currencyDataEntity.name]);
        }
        public bool HasEnough(CurrencyDataEntity currency, int amount)
        {
            return GetCurrencyAmount(currency).Amount >= amount;
        }
        public void AddCurrency(CurrencyAmount currencyAmount)
        {
            if (currencyAmount.Amount <= 0)
                throw new System.ArgumentException("Amount must be greater than zero.");

            if (!_currencyAmount.ContainsKey(currencyAmount.CurrencyDataEntity.name))
                _currencyAmount[currencyAmount.CurrencyDataEntity.name] = 0;

            _currencyAmount[currencyAmount.CurrencyDataEntity.name] += currencyAmount.Amount;
            RaiseCurrencyAmountChangedEvent(currencyAmount.CurrencyDataEntity);
        }
        public void SpendCurrency(CurrencyAmount currencyAmount)
        {
            if (currencyAmount.Amount <= 0)
                throw new System.ArgumentException("Amount must be greater than zero.");

            if (!_currencyAmount.ContainsKey(currencyAmount.CurrencyDataEntity.name) || _currencyAmount[currencyAmount.CurrencyDataEntity.name] < currencyAmount.Amount)
                throw new System.InvalidOperationException("Not enough currency to spend.");

            _currencyAmount[currencyAmount.CurrencyDataEntity.name] -= currencyAmount.Amount;
            RaiseCurrencyAmountChangedEvent(currencyAmount.CurrencyDataEntity);
        }
        public void ResetCurrency(CurrencyDataEntity currencyDataEntity)
        {
            _currencyAmount[currencyDataEntity.name] = 0;
            RaiseCurrencyAmountChangedEvent(currencyDataEntity);
        }

        private void LoadCurrencyAmounts()
        {
            _currencyAmount = new();
            // Load currency amounts from persistent storage or initialize them to default values
        }
        private void RaiseCurrencyAmountChangedEvent(CurrencyDataEntity currencyDataEntity)
        {
            _eventService.Raise(new CurrencyAmountChangedEvent(new CurrencyAmount(currencyDataEntity, _currencyAmount[currencyDataEntity.name])));
        }
    }
}
