using Rossoforge.Core.Events;
using RossoGames.LevelObjects.Components;

namespace RossoGames.LevelObjects.Events
{
    public readonly struct LevelObjectClickedEvent : IEvent
    {
        public readonly LevelObject LevelObject;

        public LevelObjectClickedEvent(LevelObject levelObject)
        {
            LevelObject = levelObject;
        }
    }
}