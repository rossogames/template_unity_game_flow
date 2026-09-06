using Rossoforge.Events.Bus;
using Rossogames.Inputs.Service;

namespace Rossogames.Inputs.Events
{
    public readonly struct InputTypeChangedEvent : IEvent
    {
        public readonly InputType InputType;

        public InputTypeChangedEvent(InputType inputType)
        {
            InputType = inputType;
        }
    }
}