using Rossoforge.Audio.Service;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Utils.StateMachine;
using Rossogames.Cameras.Service;
using Rossogames.Characters.Components;
using Rossogames.Inputs.Service;
using UnityEngine;

namespace Rossogames.Characters.States
{
    public abstract class CharacterBaseState : IState
    {
        protected IEventService _eventService;
        protected IInputsService _inputsService;
        protected ICameraService _cameraService;
        protected IAudioService _audioService;

        protected ThirdPersonController _controller;

        public CharacterBaseState(ThirdPersonController characterController)
        {
            _controller = characterController;

            _eventService = ServiceLocator.Get<IEventService>();
            _inputsService = ServiceLocator.Get<IInputsService>();
            _cameraService = ServiceLocator.Get<ICameraService>();
            _audioService = ServiceLocator.Get<IAudioService>();
        }

        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
            UpdatePosition();
            GroundedCheck();
        }

        protected Awaitable<bool> TransitionToIdle()
        {
            return _controller.StateMachine.TransitionTo(_controller.StateMachine.Idle);
        }
        protected Awaitable<bool> TransitionToMove()
        {
            return _controller.StateMachine.TransitionTo(_controller.StateMachine.Move);
        }
        protected Awaitable<bool> TransitionToJump()
        {
            return _controller.StateMachine.TransitionTo(_controller.StateMachine.Jump);
        }
        protected Awaitable<bool> TransitionToFall()
        {
            return _controller.StateMachine.TransitionTo(_controller.StateMachine.Fall);
        }

        protected bool TryToJump()
        {
            if (_inputsService.Character.Jump && _controller.Runtime.JumpTimeoutDelta <= 0.0f)
            {
                TransitionToJump();
                return true;
            }
            return false;
        }
        protected bool TryToFall()
        {
            if (!_controller.Runtime.Grounded && _controller.Runtime.VerticalVelocity <= 0.0f)
            {
                TransitionToFall();
                return true;
            }
            return false;
        }
        protected bool TryToIdle()
        {
            if (_inputsService.Character.Move == Vector2.zero && _controller.Runtime.AnimationBlend <= 0f && _controller.Runtime.Speed <= 0f)
            {
                TransitionToIdle();
                return true;
            }

            return false;
        }
        protected virtual void TryToMove()
        {
            if (_inputsService.Character.Move != Vector2.zero)
                TransitionToMove();
        }

        private void GroundedCheck()
        {
            var spherePosition = new Vector3(
                _controller.transform.position.x,
                _controller.transform.position.y - _controller.DataBehaviour.GroundedOffset,
                _controller.transform.position.z);

            _controller.Runtime.Grounded = Physics.CheckSphere(
                spherePosition,
                _controller.DataBehaviour.GroundedRadius,
                _controller.DataBehaviour.GroundLayers,
                QueryTriggerInteraction.Ignore
            );

            if (_controller.Runtime.Grounded)
            {
                _controller.Runtime.FallTimeoutDelta = _controller.DataBehaviour.FallTimeout;

                if (_controller.Runtime.VerticalVelocity < 0.0f)
                    _controller.Runtime.VerticalVelocity = -2f;

                if (_controller.Runtime.JumpTimeoutDelta >= 0.0f)
                    _controller.Runtime.JumpTimeoutDelta -= Time.deltaTime;

                _controller.SetAnimation(_controller.AnimationCache.JumpId, false);
                _controller.SetAnimation(_controller.AnimationCache.FreeFallId, false);
            }

            _controller.SetAnimation(_controller.AnimationCache.GroundedId, _controller.Runtime.Grounded);
        }
        private void UpdatePosition()
        {
            if (_controller.Runtime.VerticalVelocity < _controller.DataBehaviour.TerminalVelocity)
                _controller.Runtime.VerticalVelocity += _controller.DataBehaviour.Gravity * Time.deltaTime;

            Vector3 targetDirection = Quaternion.Euler(0.0f, _controller.Runtime.TargetRotation, 0.0f) * Vector3.forward;

            var horizontal = targetDirection.normalized * (_controller.Runtime.Speed * Time.deltaTime);
            var vertical = new Vector3(0.0f, _controller.Runtime.VerticalVelocity, 0.0f) * Time.deltaTime;
            _controller.Move(horizontal + vertical);
        }

    }
}
