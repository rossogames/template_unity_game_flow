using Rossogames.Characters.Components;
using UnityEngine;

namespace Rossogames.Characters.States
{
    public class CharacterJumpState : CharacterBaseState
    {
        public CharacterJumpState(ThirdPersonController CharacterController) : base(CharacterController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _controller.Runtime.JumpTimeoutDelta = _controller.DataBehaviour.JumpTimeout;
            _inputsService.Character.Jump = false;

            Jump();
        }
        public override void Update()
        {
            base.Update();

            if (TryToFall())
                return;

            TryToMove();
        }

        protected override void TryToMove()
        {
            if (_controller.Runtime.VerticalVelocity > 0) // still jumping, don't allow to move
                return;

            if (_controller.Runtime.Grounded)
                base.TransitionToMove();
        }

        public void Jump()
        {
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            _controller.Runtime.VerticalVelocity =
                Mathf.Sqrt(_controller.DataBehaviour.JumpHeight * -2f * _controller.DataBehaviour.Gravity);

            _controller.SetAnimation(_controller.AnimationCache.JumpId, true);
        }
    }
}
