using Rossoforge.Events.Bus;
using Rossogames.LevelObjects.DataAssets;

namespace Rossogames.LevelObjects.Events
{
    public readonly struct RoomEnterEvent : IEvent
    {
        public readonly LevelRoomDataAsset LevelRoomDataAsset;

        public RoomEnterEvent(LevelRoomDataAsset levelRoomDataAsset)
        {
            LevelRoomDataAsset = levelRoomDataAsset;
        }
    }
}
