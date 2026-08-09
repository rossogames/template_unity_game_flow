using Rossoforge.Core.Events;

namespace RossoGames.Gameplay.Events
{
    public readonly struct GameplayTokenLandedEvent : IEvent
    {
        public readonly int TileIndex;

        public GameplayTokenLandedEvent(int tileIndex)
        {
            TileIndex = tileIndex;
        }
    }
}
