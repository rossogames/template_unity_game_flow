using UnityEngine;

namespace Rossogames.LevelObjects.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(CrateDataBehaviour), menuName = "Rossogames/Data Behaviour/Level Objects/Crate")]
    public class CrateDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        public float OpenRotation { get; private set; }

        [field: SerializeField]
        public float Duration { get; private set; }

        [field: SerializeField]
        public AnimationCurve EaseCurve { get; private set; }
    }
}
