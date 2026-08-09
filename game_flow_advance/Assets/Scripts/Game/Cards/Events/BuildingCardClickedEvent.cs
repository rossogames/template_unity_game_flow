using Rossoforge.Core.Events;
using RossoGames.Buildings.DataEntities;

namespace RossoGames.Cards.Events
{
    public readonly struct BuildingCardClickedEvent : IEvent
    {
        public readonly BuildingDataEntity DataEntity;

        public BuildingCardClickedEvent(BuildingDataEntity dataEntity)
        {
            DataEntity = dataEntity;
        }
    }
}
