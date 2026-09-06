using Rossoforge.Pool.DataConfig;
using Rossogames.Level.DataAssets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Level.Service
{
    [CreateAssetMenu(fileName = nameof(LevelDataService), menuName = "Rossogames/Data Service/Level")]
    public class LevelDataService : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Level")]
        public LevelDataAsset LevelDataAsset { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Asset References")]
        [field: LabelText("Context Actions")]
        public PooledGameobjectDataConfig ContextActionsAssetReference { get; private set; }
    }
}