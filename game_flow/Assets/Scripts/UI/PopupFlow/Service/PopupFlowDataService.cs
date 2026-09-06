using Rossoforge.Pool.DataConfig;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.PopupFlow.Service
{
    [CreateAssetMenu(fileName = nameof(PopupFlowDataService), menuName = "Rossogames/Data Service/PopupFlow")]
    public class PopupFlowDataService : ScriptableObject
    {
        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Pause")]
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupPauseAssetReference { get; private set; }

        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Question")]
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupQuestionAssetReference { get; private set; }

        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Settings")]
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupSettingsAssetReference { get; private set; }

        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Container")]
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupContainerAssetReference { get; private set; }

        [field: BoxGroup("Popup Asset Reference")]
        [field: LabelText("Inventory")]
        [field: SerializeField]
        public PooledGameobjectDataConfig PopupInventoryAssetReference { get; private set; }
    }
}
