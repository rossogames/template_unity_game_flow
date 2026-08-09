using Rossoforge.Core.Events;
using RossoGames.Level.DataAssets;

namespace RossoGames.Level.Events
{
    public readonly struct LevelLoadedEvent : IEvent
    {
        public readonly LevelDataAsset LevelDataAsset;

        public LevelLoadedEvent(LevelDataAsset levelDataAsset)
        {
            LevelDataAsset = levelDataAsset;
        }
    }
}