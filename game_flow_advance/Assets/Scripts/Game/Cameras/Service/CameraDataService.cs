using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Cameras.Service
{
    [CreateAssetMenu(fileName = nameof(CameraDataService), menuName = "RossoGames/Service Data/Camera")]
    public class CameraDataService : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Rotation")]
        [field: LabelText("Sensitivity")]
        public float SensitivityRotation { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Movement")]
        [field: LabelText("Sensitivity")]
        public float SensitivityMove { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Zoom")]
        [field: LabelText("Sensitivity")]
        public float SensitivityZoom { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Zoom")]
        [field: LabelText("Range")]
        public RangeNumber<float> RangeZoom { get; private set; }
    }
}