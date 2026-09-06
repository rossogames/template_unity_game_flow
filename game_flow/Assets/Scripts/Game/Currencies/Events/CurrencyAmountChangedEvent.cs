using Rossoforge.Events.Bus;
using Rossogames.Currencies.DataStructures;

namespace Rossogames.Currencies.Events
{
    public struct CurrencyAmountChangedEvent : IEvent
    {
        public CurrencyAmountChangedEvent(CurrencyAmount amount)
        {
            Amount = amount;
        }

        public CurrencyAmount Amount { get; private set; }
    }
}
