using Rossoforge.Core.Events;
using RossoGames.LevelObjects.Components;

namespace RossoGames.LevelObjects.Events
{
    public readonly struct LevelObjectTargetedEvent : IEvent
    {
        public readonly LevelObject LevelObject;

        public LevelObjectTargetedEvent(LevelObject levelObject)
        {
            LevelObject = levelObject;
        }
    }
}