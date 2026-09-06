using Rossogames.Characters.Components;
using UnityEngine;

namespace Rossogames.Characters.States
{
    public class CharacterMoveState : CharacterBaseState
    {
        public CharacterMoveState(ThirdPersonController CharacterController) : base(CharacterController)
        {
        }

        public override void Update()
        {
            base.Update();

            if (TryToJump())
                return;

            if (TryToFall())
                return;

            TryToIdle();
            Move();
        }

        private void Move()
        {
            float targetSpeed = _inputsService.Character.Sprint ?
                _controller.DataBehaviour.SprintSpeed :
                _controller.DataBehaviour.MoveSpeed;

            if (_inputsService.Character.Move == Vector2.zero) // if there is no input, set the target speed to 0
                targetSpeed = 0.0f;

            SetCurrentSpeed(targetSpeed);
            SetAnimationBlend(targetSpeed);

            Vector3 inputDirection = new Vector3(_inputsService.Character.Move.x, 0.0f, _inputsService.Character.Move.y).normalized;
            if (_inputsService.Character.Move != Vector2.zero)
            {
                SetTargetRotation(inputDirection);
                SetRotation();
            }

            SetAnimation();
        }
        private void SetCurrentSpeed(float targetSpeed)
        {
            float currentHorizontalSpeed = new Vector3(_controller.Velocity.x, 0.0f, _controller.Velocity.z).magnitude;
            float speedOffset = 0.1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _controller.Runtime.Speed = Mathf.Lerp(
                    currentHorizontalSpeed,
                    targetSpeed * _inputsService.Character.Move.magnitude,
                    Time.deltaTime * _controller.DataBehaviour.SpeedChangeRate
                );
                _controller.Runtime.Speed = Mathf.Round(_controller.Runtime.Speed * 1000f) / 1000f;
            }
            else
                _controller.Runtime.Speed = targetSpeed;
        }
        private void SetAnimationBlend(float targetSpeed)
        {
            _controller.Runtime.AnimationBlend = Mathf.Lerp(
                _controller.Runtime.AnimationBlend,
                targetSpeed,
                Time.deltaTime * _controller.DataBehaviour.SpeedChangeRate
            );

            if (_controller.Runtime.AnimationBlend < 0.01f)
                _controller.Runtime.AnimationBlend = 0f;
        }
        private void SetTargetRotation(Vector3 inputDirection)
        {
            _controller.Runtime.TargetRotation =
                Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                _cameraService.Camera.transform.eulerAngles.y;
        }
        private void SetRotation()
        {
            float rotation = Mathf.LerpAngle(
                _controller.transform.eulerAngles.y,
                _controller.Runtime.TargetRotation,
                Time.deltaTime * _controller.DataBehaviour.RotationChangeRate
            );

            _controller.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        private void SetAnimation()
        {
            var motionSpeed = Mathf.Clamp01(_inputsService.Character.Move.magnitude);

            _controller.SetAnimation(_controller.AnimationCache.SpeedId, _controller.Runtime.AnimationBlend);
            _controller.SetAnimation(_controller.AnimationCache.MotionSpeedId, motionSpeed);
        }
    }
}
