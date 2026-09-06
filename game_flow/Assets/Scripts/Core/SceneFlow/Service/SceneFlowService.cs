using Rossoforge.Scenes.DataConfig;
using Rossoforge.Scenes.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossoforge.Utils.Logger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.SceneFlow.Service
{
    public class SceneFlowService : ISceneFlowService, IInitializable
    {
        private ISceneService _sceneService;
        private SceneFlowDataService _dataService;

        private Dictionary<SceneTransitionType, SceneTransitionDataConfig> _transitionsMap = new();

        public SceneFlowService(SceneFlowDataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            if (_dataService == null)
            {
                RossoLogger.Error($"{nameof(SceneFlowDataService)} not assigned");
                return;
            }

            _sceneService = ServiceLocator.Get<ISceneService>();
            InitializeTransitionMapper();
        }

        public Awaitable GoToMainScene(SceneTransitionType? transitionType = null, Func<Awaitable> onScreenCoveredAsync = null)
        {
            return ChangeScene(_dataService.SceneNames.Main, transitionType, onScreenCoveredAsync);
        }
        public Awaitable GoToGamePlayScene(SceneTransitionType? transitionType = null)
        {
            return ChangeScene(_dataService.SceneNames.GamePlay, transitionType);
        }

        private Awaitable ChangeScene(string sceneName, SceneTransitionType? transitionType = null, Func<Awaitable> onScreenCoveredAsync = null)
        {
            SceneTransitionDataConfig transitionData = GetTransitionData(transitionType);

            if (transitionData != null)
                return _sceneService.ChangeScene(sceneName, transitionData, onScreenCoveredAsync);
            else
                return _sceneService.ChangeScene(sceneName, onScreenCoveredAsync); // use default transition
        }

        private void InitializeTransitionMapper()
        {
            if (_dataService.SceneTransitions == null || _dataService.SceneTransitions.Length == 0)
            {
                RossoLogger.Warning($"No transitions configured in {nameof(SceneTransitionDataConfig)}");
                return;
            }

            foreach (var entry in _dataService.SceneTransitions)
            {
                if (_transitionsMap.ContainsKey(entry.Type))
                {
                    RossoLogger.Warning($"Duplicate transition type: {entry.Type}");
                    continue;
                }

                _transitionsMap.Add(entry.Type, entry.Data);
            }
        }

        private SceneTransitionDataConfig GetTransitionData(SceneTransitionType? transitionType = null)
        {
            SceneTransitionDataConfig transitionData = null;
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
