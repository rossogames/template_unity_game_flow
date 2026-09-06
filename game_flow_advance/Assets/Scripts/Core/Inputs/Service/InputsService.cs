using RossoGames.Inputs.Events;
using UnityEngine.InputSystem;

namespace RossoGames.Inputs.Service
{
    public class InputsService : IInputsService, InputActionsControls.IGameActions
    {
        private IEventService _eventService;

        private InputActionsControls _controls;

        public float HorizontalAxis { get; private set; }

        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            _controls = new InputActionsControls();

            _controls.Game.Enable();
            _controls.Game.SetCallbacks(this);
        }

        public void OnCancel(InputAction.CallbackContext context) => KeyPressed<CancelInputPressedEvent>(context);

        private void KeyPressed<T>(InputAction.CallbackContext context) where T : IEvent
        {
            if (context.performed)
                _eventService.Raise<T>();
        }
    }
}