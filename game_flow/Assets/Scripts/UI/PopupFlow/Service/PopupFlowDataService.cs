using Rossoforge.Pool.DataConfig;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Rossogames.PopupFlow.Service
{
    [CreateAssetMenu(fileName = nameof(PopupFlowDataService), menuName = "Rossogames/Data Service/PopupFlow")]
    public class PopupFlowDataService : ScriptableObject
    {
#if ODIN_INSPECTOR
        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Pause")]
#endif
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupPauseAssetReference { get; private set; }

#if ODIN_INSPECTOR
        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Question")]
#endif
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupQuestionAssetReference { get; private set; }

#if ODIN_INSPECTOR
        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Settings")]
#endif
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupSettingsAssetReference { get; private set; }
    }
}
