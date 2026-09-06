using RossoGames.Gameplay.Phases;

namespace RossoGames.Gameplay.Events
{
    public readonly struct GameplayPhaseChangedEvent : IEvent
    {
        public readonly GameplayBasePhase CurrentPhase;

        public GameplayPhaseChangedEvent(GameplayBasePhase currentPhase)
        {
            CurrentPhase = currentPhase;
        }
    }
}
