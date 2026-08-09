using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Gameplay.DataBehaviour
{
    public abstract class GameplayBasePhaseDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("General")]
        public bool AllowPause { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Camera")]
        public bool AllowCameraMove { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Camera")]
        public bool AllowCameraRotate { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Camera")]
        public bool AllowCameraZoom { get; private set; }
    }
}
