using Rossogames.Characters.DataBehavior;

namespace Rossogames.Characters.DataRuntime
{
    public class CharacterDataRuntime
    {
        public float Speed { get; set; }
        public float AnimationBlend { get; set; }
        public float TargetRotation { get; set; }
        public float VerticalVelocity { get; set; }
        public float JumpTimeoutDelta { get; set; }
        public float FallTimeoutDelta { get; set; }
        public bool Grounded { get; set; }

        public CharacterDataRuntime(CharacterDataBehaviour dataBehaviour)
        {
            Grounded = true;
            JumpTimeoutDelta = dataBehaviour.JumpTimeout;
            FallTimeoutDelta = dataBehaviour.FallTimeout;
        }
    }
}