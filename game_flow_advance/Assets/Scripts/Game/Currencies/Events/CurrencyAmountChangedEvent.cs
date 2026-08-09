using Rossoforge.Core.Events;
using RossoGames.Currencies.DataTypes;

namespace RossoGames.Currencies.Events
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
