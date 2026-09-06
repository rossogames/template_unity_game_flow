using Rossoforge.Events.Bus;
using Rossogames.Inputs.Enums;

namespace Rossogames.Inputs.Events
{
    public readonly struct ContextActionInputPressedEvent : IEvent
    {
        public readonly ContextActionInput ActionInput;

        public ContextActionInputPressedEvent(ContextActionInput actionInput)
        {
            ActionInput = actionInput;
        }
    }
}