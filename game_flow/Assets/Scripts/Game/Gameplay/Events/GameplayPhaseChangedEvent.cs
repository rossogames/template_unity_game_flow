using Rossoforge.Events.Bus;
using Rossogames.Gameplay.Phases;

namespace Rossogames.Gameplay.Events
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
