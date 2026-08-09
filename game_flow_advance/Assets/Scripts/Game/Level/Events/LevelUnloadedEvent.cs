using Rossoforge.Core.Events;
using RossoGames.Level.DataAssets;

namespace RossoGames.Level.Events
{
    public readonly struct LevelUnloadedEvent : IEvent
    {
        public readonly LevelDataAsset LevelDataAsset;

        public LevelUnloadedEvent(LevelDataAsset levelDataAsset)
        {
            this.LevelDataAsset = levelDataAsset;
        }
    }
}