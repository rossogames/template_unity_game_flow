using System;
using UnityEngine;

namespace RossoGames.PopupFlow.Service
{
    [CreateAssetMenu(fileName = nameof(PopupFlowDataService), menuName = "RossoGames/Service Data/PopupFlow")]
    public class PopupFlowDataService : ScriptableObject
    {
        [field: SerializeField]
        public PopupEntry[] Popups { get; private set; }
    }

    [Serializable]
    public class PopupEntry
    {
        [field: SerializeField]
        public PopupType Type { get; private set; }

        [field: SerializeField]
        public PooledGameobjectData AssetReference { get; private set; }
    }
}
