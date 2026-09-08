using Rossoforge.Pool.DataConfig;
using Rossogames.Level.DataAssets;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Rossogames.Level.Service
{
    [CreateAssetMenu(fileName = nameof(LevelDataService), menuName = "Rossogames/Data Service/Level")]
    public class LevelDataService : ScriptableObject
    {
        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Data Asset")]
        [field: LabelText("Level")]
#endif
        public LevelDataAsset LevelDataAsset { get; private set; }

        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Asset References")]
        [field: LabelText("Context Actions")]
#endif
        public PooledGameobjectDataConfig ContextActionsAssetReference { get; private set; }
    }
}