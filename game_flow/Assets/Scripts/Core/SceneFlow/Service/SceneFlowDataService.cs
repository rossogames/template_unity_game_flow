using Rossoforge.Scenes.DataConfig;
using System;
using UnityEngine;

namespace Rossogames.SceneFlow.Service
{
    [CreateAssetMenu(fileName = nameof(SceneFlowDataService), menuName = "Rossogames/Data Service/SceneFlow")]
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
    }

    [Serializable]
    public class SceneTransitionEntry
    {
        [field: SerializeField]
        public SceneTransitionType Type { get; private set; }

        [field: SerializeField]
        public SceneTransitionDataConfig Data { get; private set; }
    }
}
