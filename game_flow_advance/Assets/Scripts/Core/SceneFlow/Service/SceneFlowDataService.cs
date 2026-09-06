using System;
using UnityEngine;

namespace RossoGames.SceneFlow.Service
{
    [CreateAssetMenu(fileName = nameof(SceneFlowDataService), menuName = "RossoGames/Service Data/SceneFlow")]
    public class SceneFlowDataService : ScriptableObject
    {
        [field: SerializeField]
        public SceneNames SceneNames { get; private set; }

        [field: SerializeField]
        public SceneTransitionEntry[] SceneTransitions { get; private set; }
    }

    [Serializable]
    public class SceneNames
    {
        [field: SerializeField]
        public string Main { get; private set; }

        [field: SerializeField]
        public string GamePlay { get; private set; }

        [field: SerializeField]
        public string GamePlayUnload { get; private set; }
    }

    [Serializable]
    public class SceneTransitionEntry
    {
        [field: SerializeField]
        public SceneTransitionType Type { get; private set; }

        [field: SerializeField]
        public SceneTransitionData Data { get; private set; }
    }
}
