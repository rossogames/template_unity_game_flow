using Rossoforge.Services.Service;
using System;
using UnityEngine;

namespace Rossogames.SceneFlow.Service
{
    public interface ISceneFlowService : IService
    {
        Awaitable GoToMainScene(SceneTransitionType? transitionType = null, Func<Awaitable> onScreenCoveredAsync = null);
        Awaitable GoToGamePlayScene(SceneTransitionType? transitionType = null);
    }
}
