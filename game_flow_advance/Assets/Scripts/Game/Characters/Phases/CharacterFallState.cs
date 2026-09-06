using Rossogames.Characters.Components;
using UnityEngine;

namespace Rossogames.Characters.States
{
    public class CharacterFallState : CharacterBaseState
    {
        public CharacterFallState(ThirdPersonController CharacterController) : base(CharacterController)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            base.Update();

            Falling();
            TryToMove();
        }

        protected override void TryToMove()
        {
            if (_controller.Runtime.Grounded)
                base.TransitionToMove();
        }

        private void Falling()
        {
            if (_controller.Runtime.FallTimeoutDelta >= 0.0f)
                _controller.Runtime.FallTimeoutDelta -= Time.deltaTime;
            else
                _controller.SetAnimation(_controller.AnimationCache.FreeFallId, true);
        }
    }
}
