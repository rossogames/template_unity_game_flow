using Rossogames.Characters.Components;

namespace Rossogames.Characters.States
{
    public class CharacterIdleState : CharacterBaseState
    {
        public CharacterIdleState(ThirdPersonController CharacterController) : base(CharacterController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _controller.Runtime.AnimationBlend = 0f;
            _controller.Runtime.Speed = 0f;
        }

        public override void Update()
        {
            base.Update();

            if (TryToJump())
                return;

            TryToMove();
        }
    }
}
