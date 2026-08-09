using Rossoforge.Core.Events;

namespace RossoGames.Gameplay.Events
{
    public readonly struct GameplayRollDiceEndedEvent : IEvent
    {
        public readonly int DiceValue;

        public GameplayRollDiceEndedEvent(int diceValue)
        {
            DiceValue = diceValue;
        }
    }
}
