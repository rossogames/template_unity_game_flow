using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Inputs.Enums;
using Rossogames.Inputs.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rossogames.Inputs.Service
{
    public class InputsService : IInputsService, IInitializable, ILateUpdatable,
        InputActionsControls.IGameActions
    {
        private IEventService _eventService;
        private Vector2 _lastCursorPosition;
        private CursorButton _pressedButton;

        private bool _isDragging;
        private Vector2 _mouseMoveDelta;

        // movement throttling / threshold to avoid very high-frequency events
        private const float MoveEpsilon = 0.5f; // pixels
        private const float MoveEpsilonSq = MoveEpsilon * MoveEpsilon;

        private InputActionsControls _controls;

        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            _controls = new InputActionsControls();
            _controls.Game.Enable();
            _controls.Game.SetCallbacks(this);
        }

        public void LateUpdate()
        {
            _mouseMoveDelta = Vector2.zero;
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            KeyPressed<PauseInputPressedEvent>(context);
        }
        public void OnCancel(InputAction.CallbackContext context)
        {
            KeyPressed<CancelInputPressedEvent>(context);
        }

        // -- CURSOR --
        public void OnRightCursorClick(InputAction.CallbackContext context)
        {
            CursorClick(context, CursorButton.Right);
        }

        public void OnCursorPosition(InputAction.CallbackContext context)
        {
            CursorMove(context);
        }

        private void KeyPressed<T>(InputAction.CallbackContext context) where T : IEvent
        {
            if (context.performed)
                _eventService.Raise<T>();
        }
        private void KeyPressed<T>(InputAction.CallbackContext context, T arg) where T : IEvent
        {
            if (context.performed)
                _eventService.Raise(arg);
        }
        private void CursorClick(InputAction.CallbackContext context, CursorButton button)
        {
            if (context.performed)
                _pressedButton |= button;
            else if (context.canceled)
                _pressedButton &= ~button; // clear the flag for this button in a safe bitwise way
        }
        private void CursorMove(InputAction.CallbackContext context)
        {
            var currentCursorPosition = context.ReadValue<Vector2>();
            if (currentCursorPosition.x < 0 || currentCursorPosition.y < 0)
                return;

            _mouseMoveDelta = currentCursorPosition - _lastCursorPosition;
            if (_mouseMoveDelta.sqrMagnitude <= MoveEpsilonSq) // skip very small movements
            {
                _lastCursorPosition = currentCursorPosition;
                return;
            }

            if (_pressedButton != CursorButton.None)
            {
                _isDragging = true;
            }
            else if (_isDragging)
            {
                _isDragging = false;
            }

            _lastCursorPosition = currentCursorPosition;
        }
    }
}