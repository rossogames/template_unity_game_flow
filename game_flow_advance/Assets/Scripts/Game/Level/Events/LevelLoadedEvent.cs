using Rossoforge.Events.Bus;
using Rossogames.Level.DataAssets;

namespace Rossogames.Level.Events
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