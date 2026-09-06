using Rossogames.LevelObjects.Components;
using UnityEngine;

namespace Rossogames.LevelObjects.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(OutlineDataBehaviour), menuName = "Rossogames/Data Behaviour/Outline")]
    public class OutlineDataBehaviour : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
        [field: SerializeField] public Mode Mode { get; private set; }
    }
}
