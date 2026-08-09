using RossoGames.LevelObjects.Components;
using UnityEngine;

namespace RossoGames.LevelObjects.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(OutlineDataBehaviour), menuName = "RossoGames/Data Behaviour/Outline")]
    public class OutlineDataBehaviour : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
        [field: SerializeField] public Mode Mode { get; private set; }
    }
}
