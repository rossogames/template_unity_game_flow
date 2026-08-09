using Rossoforge.Core.Scenes;
using Rossoforge.Core.Services;
using Rossoforge.Scenes.Data;
using Rossoforge.Services;
using Rossoforge.Utils.Logger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RossoGames.SceneFlow.Service
{
    public class SceneFlowService : ISceneFlowService, IInitializable
    {
        private ISceneService _sceneService;
        private SceneFlowServiceData _serviceData;

        private Dictionary<SceneTransitionType, SceneTransitionData> _transitionsMap = new();

        public SceneFlowService(SceneFlowServiceData serviceData)
        {
            _serviceData = serviceData;
        }
        public void Initialize()
        {
            if (_serviceData == null)
            {
                RossoLogger.Error($"{nameof(SceneFlowServiceData)} not assigned");
                return;
            }

            _sceneService = ServiceLocator.Get<ISceneService>();
            InitializeTransitionMapper();
        }

        public Awaitable GoToMainScene(SceneTransitionType? transitionType = null, Func<Awaitable> onScreenCoveredAsync = null)
        {
            return ChangeScene(_serviceData.SceneNames.Main, transitionType, onScreenCoveredAsync);
        }
        public Awaitable GoToGamePlayScene(SceneTransitionType? transitionType = null)
        {
            return ChangeScene(_serviceData.SceneNames.GamePlay, transitionType);
        }

        private Awaitable ChangeScene(string sceneName, SceneTransitionType? transitionType = null, Func<Awaitable> onScreenCoveredAsync = null)
        {
            SceneTransitionData transitionData = GetTransitionData(transitionType);

            if (transitionData != null)
                return _sceneService.ChangeScene(sceneName, transitionData, onScreenCoveredAsync);
            else
                return _sceneService.ChangeScene(sceneName, onScreenCoveredAsync); // use default transition
        }

        private void InitializeTransitionMapper()
        {
            if (_serviceData.SceneTransitions == null || _serviceData.SceneTransitions.Length == 0)
            {
                RossoLogger.Warning($"No transitions configured in {nameof(SceneTransitionData)}");
                return;
            }

            foreach (var entry in _serviceData.SceneTransitions)
            {
                if (_transitionsMap.ContainsKey(entry.Type))
                {
                    RossoLogger.Warning($"Duplicate transition type: {entry.Type}");
                    continue;
                }

                _transitionsMap.Add(entry.Type, entry.Data);
            }
        }

        private SceneTransitionData GetTransitionData(SceneTransitionType? transitionType = null)
        {
            SceneTransitionData transitionData = null;
            if (transitionType.HasValue)
            {
                _transitionsMap.TryGetValue(transitionType.Value, out transitionData);

                if (transitionData == null)
                    RossoLogger.Warning($"Transition '{transitionType}' not found. Using default.");
            }

            return transitionData;
        }
    }
}
