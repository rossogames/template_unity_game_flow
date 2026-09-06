using RossoGames.Level.DataAssets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Level.Service
{
    [CreateAssetMenu(fileName = nameof(LevelDataService), menuName = "RossoGames/Service Data/Level")]
    public class LevelDataService : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Current Level")]
        public LevelDataAsset CurrentLevelDataAsset { get; private set; }
    }
}