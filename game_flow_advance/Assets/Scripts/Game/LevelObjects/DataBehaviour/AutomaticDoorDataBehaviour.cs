using UnityEngine;

namespace Rossogames.LevelObjects.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(AutomaticDoorDataBehaviour), menuName = "Rossogames/Data Behaviour/Level Objects/Automatic Door")]
    public class AutomaticDoorDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        public float OpenDistance { get; private set; }

        [field: SerializeField]
        public float Duration { get; private set; }

        [field: SerializeField]
        public AnimationCurve EaseCurve { get; private set; }
    }
}
