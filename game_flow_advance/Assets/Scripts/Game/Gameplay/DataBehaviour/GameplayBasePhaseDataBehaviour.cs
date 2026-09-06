using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Gameplay.DataBehaviour
{
    public abstract class GameplayBasePhaseDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("General")]
        public bool AllowPause { get; private set; } = true;

        [field: SerializeField]
        [field: BoxGroup("Camera")]
        public bool AllowMove { get; private set; } = true;

        [field: SerializeField]
        [field: BoxGroup("Camera")]
        public bool AllowRotate { get; private set; } = true;
    }
}
