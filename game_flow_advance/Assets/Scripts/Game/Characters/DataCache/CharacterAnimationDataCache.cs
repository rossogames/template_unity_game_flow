using UnityEngine;

namespace Rossogames.Characters.DataCache
{
    public class CharacterAnimationDataCache
    {
        public int SpeedId { get; private set; }
        public int GroundedId { get; private set; }
        public int JumpId { get; private set; }
        public int FreeFallId { get; private set; }
        public int MotionSpeedId { get; private set; }

        public CharacterAnimationDataCache()
        {
            SpeedId = Animator.StringToHash("Speed");
            GroundedId = Animator.StringToHash("Grounded");
            JumpId = Animator.StringToHash("Jump");
            FreeFallId = Animator.StringToHash("FreeFall");
            MotionSpeedId = Animator.StringToHash("MotionSpeed");
        }
    }
}