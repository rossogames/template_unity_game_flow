using Rossoforge.Core.Services;
using RossoGames.Currencies.DataEntities;
using RossoGames.Currencies.DataTypes;

namespace RossoGames.Currencies.Service
{
    public interface ICurrencyService : IService, IInitializable
    {
        CurrencyAmount GetCurrencyAmount(CurrencyDataEntity currencyDataEntity);
        bool HasEnough(CurrencyDataEntity currency, int amount);
        void AddCurrency(CurrencyAmount currencyAmount);
        void SpendCurrency(CurrencyAmount currencyAmount);
        void ResetCurrency(CurrencyDataEntity currencyDataEntity);
    }
}
