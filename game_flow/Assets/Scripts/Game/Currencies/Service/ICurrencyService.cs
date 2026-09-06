using Rossoforge.Services.Service;
using Rossogames.Currencies.DataEntities;
using Rossogames.Currencies.DataStructures;

namespace Rossogames.Currencies.Service
{
    public interface ICurrencyService : IService
    {
        CurrencyAmount GetCurrencyAmount(CurrencyDataEntity currencyDataEntity);
        bool HasEnough(CurrencyDataEntity currency, int amount);
        void AddCurrency(CurrencyAmount currencyAmount);
        void SpendCurrency(CurrencyAmount currencyAmount);
        void ResetCurrency(CurrencyDataEntity currencyDataEntity);
    }
}
