using Rossoforge.Core.Services;

namespace RossoGames.SceneFlow.Service
{
    public interface ISceneFlowService : IService
    {
        void GoToMainScene(SceneTransitionType? transitionType = null);
        void GoToGamePlayScene(SceneTransitionType? transitionType = null);

        void LoadGameplayUnloadScene();
        void LoadMainScene();


        void UnloadGameplayScene();
        void UnloadGameplayUnloadScene();
    }
}
