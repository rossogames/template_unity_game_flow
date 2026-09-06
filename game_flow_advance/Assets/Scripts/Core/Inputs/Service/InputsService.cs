using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Inputs.Enums;
using Rossogames.Inputs.Events;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rossogames.Inputs.Service
{
    public class InputsService : IInputsService, IInitializable, IDisposable, IUpdatable, ILateUpdatable,
        InputActionsControls.IGameActions
    {
        private IEventService _eventService;
        private Vector2 _lastCursorPosition;
        private CursorButton _pressedButton;

        private bool _isDragging;
        private Vector2 _mouseMoveDelta;
        private InputType _currentInputType;

        // movement throttling / threshold to avoid very high-frequency events
        private const float MoveEpsilon = 0.5f; // pixels
        private const float MoveEpsilonSq = MoveEpsilon * MoveEpsilon;

        private InputActionsControls _controls;

        public InputType CurrentInputType
        {
            get => _currentInputType;
            private set
            {
                if (_currentInputType == value)
                    return;

                _currentInputType = value;
                _eventService.Raise(new InputTypeChangedEvent(value));
            }
        }

        public CharacterInputs Character { get; private set; }
        public CameraInputs Camera { get; private set; }

        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            Character = new();
            Camera = new();

            _controls = new InputActionsControls();
            _controls.Game.Enable();
            _controls.Game.SetCallbacks(this);

            InputSystem.onActionChange += OnActionChange;
        }
        public void Dispose()
        {
            InputSystem.onActionChange -= OnActionChange;
        }
        public void Update()
        {
            TryRotateCameraOnDrag();
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

        // -- CAMERA --
        public void OnCameraRotation(InputAction.CallbackContext context)
        {
            Camera.RotationJoystick = context.ReadValue<float>();
        }

        // -- CHARACTER --
        public void OnMove(InputAction.CallbackContext context)
        {
            Character.Move = context.ReadValue<Vector2>();
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            Character.Sprint = context.ReadValueAsButton();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            Character.Jump = context.ReadValueAsButton();
        }

        // -- CONTEXT ACTION --
        public void OnContextAction1(InputAction.CallbackContext context)
        {
            KeyPressed(context, new ContextActionInputPressedEvent(ContextActionInput.Input1));
        }
        public void OnContextAction2(InputAction.CallbackContext context)
        {
            KeyPressed(context, new ContextActionInputPressedEvent(ContextActionInput.Input2));
        }
        public void OnContextAction3(InputAction.CallbackContext context)
        {
            KeyPressed(context, new ContextActionInputPressedEvent(ContextActionInput.Input3));
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
        private void TryRotateCameraOnDrag()
        {
            if (!_isDragging || _pressedButton != CursorButton.Right)
            {
                this.Camera.IsMouseRotation = false;
                this.Camera.RotationMouse = 0;
                return;
            }

            this.Camera.IsMouseRotation = true;
            this.Camera.RotationMouse = _mouseMoveDelta.x;
        }
        private void OnActionChange(object obj, InputActionChange change)
        {
            if (change == InputActionChange.ActionPerformed)
            {
                InputAction action = (InputAction)obj;
                CurrentInputType =
                      action.activeControl.device is Gamepad ? InputType.Controller :
                      InputType.KeyboardMouse;
            }
        }
    }
    public enum InputType
    {
        KeyboardMouse,
        Controller
    }
    public class CameraInputs
    {
        public bool IsMouseRotation { get; set; }
        public float RotationMouse { get; set; }
        public float RotationJoystick { get; set; }
    }

    public class CharacterInputs
    {
        public Vector2 Move { get; set; }
        public bool Jump { get; set; }
        public bool Sprint { get; set; }
    }
}