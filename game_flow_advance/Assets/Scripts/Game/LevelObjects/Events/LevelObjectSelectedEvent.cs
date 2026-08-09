using Rossoforge.Core.Events;
using RossoGames.LevelObjects.Components;

namespace RossoGames.LevelObjects.Events
{
    public readonly struct LevelObjectSelectedEvent : IEvent
    {
        public readonly LevelObject LevelObject;

        public LevelObjectSelectedEvent(LevelObject levelObject)
        {
            LevelObject = levelObject;
        }
    }
}