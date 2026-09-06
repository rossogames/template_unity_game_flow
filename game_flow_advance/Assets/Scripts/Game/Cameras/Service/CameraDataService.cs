using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Cameras.Service
{
    [CreateAssetMenu(fileName = nameof(CameraDataService), menuName = "Rossogames/Data Service/Camera")]
    public class CameraDataService : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Height")]
        [field: LabelText("Range")]
        public float Height { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Rotation")]
        public float InitRotationX { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Rotation")]
        public float InitRotationY { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Rotation")]
        [field: LabelText("Sensitivity Joystick")]
        public float SensitivityRotationJoystick { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Rotation")]
        [field: LabelText("Sensitivity Mouse")]
        public float SensitivityRotationMouse { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Movement")]
        [field: LabelText("Sensitivity")]
        public float SensitivityMove { get; private set; }

        /*
        [field: SerializeField]
        [field: BoxGroup("Zoom")]
        [field: LabelText("Sensitivity")]
        public float SensitivityZoom { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Zoom")]
        [field: LabelText("Range")]
        public RangeNumber<float> RangeZoom { get; private set; }
        */
    }
}