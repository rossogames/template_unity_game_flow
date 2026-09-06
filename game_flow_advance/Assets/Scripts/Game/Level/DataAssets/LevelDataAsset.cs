using Rossogames.LevelObjects.DataAssets;
using UnityEngine;

namespace Rossogames.Level.DataAssets
{
    [CreateAssetMenu(fileName = nameof(LevelDataAsset), menuName = "Rossogames/Data Assets/Level")]
    public class LevelDataAsset : ScriptableObject
    {
        [field: SerializeField]
        public LevelRoomDataAsset[] Rooms { get; private set; }
    }
}
