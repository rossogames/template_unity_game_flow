using Rossoforge.Utils.StateMachine;
using Rossogames.Characters.Components;
using UnityEngine;

namespace Rossogames.Characters.States
{
    public class CharacterStateMachine : StateMachine<CharacterBaseState>
    {
        public CharacterIdleState Idle { get; private set; }
        public CharacterMoveState Move { get; private set; }
        public CharacterJumpState Jump { get; private set; }
        public CharacterFallState Fall { get; private set; }

        public CharacterStateMachine(ThirdPersonController CharacterController)
        {
            Idle = new CharacterIdleState(CharacterController);
            Move = new CharacterMoveState(CharacterController);
            Jump = new CharacterJumpState(CharacterController);
            Fall = new CharacterFallState(CharacterController);
        }

        public override async Awaitable<bool> TransitionTo(CharacterBaseState nextState)
        {
            if (!await base.TransitionTo(nextState))
                return false;

            return true;
        }
    }
}
