using RossoGames.Level.DataAssets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Level.Service
{
    [CreateAssetMenu(fileName = nameof(LevelServiceData), menuName = "RossoGames/Service Data/Level")]
    public class LevelServiceData : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Current Level")]
        public LevelDataAsset CurrentLevelDataAsset { get; private set; }
    }
}