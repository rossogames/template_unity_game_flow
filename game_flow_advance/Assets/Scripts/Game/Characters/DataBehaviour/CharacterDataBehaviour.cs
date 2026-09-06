using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Characters.DataBehavior
{
    [CreateAssetMenu(fileName = nameof(CharacterDataBehaviour), menuName = "Rossogames/Data Behaviour/Character")]
    public class CharacterDataBehaviour : ScriptableObject
    {
        [field: BoxGroup("Movement")]
        [field: Tooltip("Move speed of the character in m/s")]
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 2.0f;

        [field: BoxGroup("Movement")]
        [field: Tooltip("Sprint speed of the character in m/s")]
        [field: SerializeField]
        public float SprintSpeed { get; private set; } = 5.335f;

        [field: BoxGroup("Movement")]
        [field: SerializeField]
        public float RotationChangeRate { get; private set; }

        [field: BoxGroup("Movement")]
        [field: Tooltip("Acceleration and deceleration")]
        [field: SerializeField]
        public float SpeedChangeRate { get; private set; } = 10.0f;

        [field: BoxGroup("Jumping")]
        [field: Tooltip("The height the player can jump")]
        [field: SerializeField]
        public float JumpHeight { get; private set; } = 1.2f;

        [field: BoxGroup("Jumping")]
        [field: Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        [field: SerializeField]
        public float JumpTimeout { get; private set; } = 0.50f;

        [field: BoxGroup("Falling")]
        [field: Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        [field: SerializeField]
        public float Gravity { get; private set; } = -15.0f;

        [field: BoxGroup("Falling")]
        [field: Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        [field: SerializeField]
        public float FallTimeout { get; private set; } = 0.15f;

        [field: BoxGroup("Falling")]
        [field: Tooltip("The maximum velocity the character can reach while falling")]
        [field: SerializeField]
        public float TerminalVelocity { get; private set; } = 53.0f;

        [field: BoxGroup("Ground")]
        [field: Tooltip("Useful for rough ground")]
        [field: SerializeField]
        public float GroundedOffset { get; private set; } = -0.14f;

        [field: BoxGroup("Ground")]
        [field: Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius { get; private set; } = 0.28f;

        [field: BoxGroup("Ground")]
        [field: Tooltip("What layers the character uses as ground")]
        [field: SerializeField]
        public LayerMask GroundLayers { get; private set; }
    }
}
