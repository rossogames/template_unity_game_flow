using Rossoforge.Core.Events;
using Rossoforge.Services;
using RossoGames.Gameplay.Events;
using RossoGames.SceneFlow.Service;
using UnityEngine;

namespace RossoGames.Gameplay.Components
{
    [RequireComponent(typeof(Animator))]
    public class GameplayUnload : MonoBehaviour, IEventListener<GameplayFinishEvent>
    {
        [HideInInspector] public Animator Animator;

        private IEventService _eventService;
        private ISceneFlowService _sceneFlowService;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();

            _eventService.RegisterListener<GameplayFinishEvent>(this);

            Animator = GetComponent<Animator>();
        }
        private void OnDestroy()
        {
            _eventService.UnregisterListener<GameplayFinishEvent>(this);
        }

        public void OnTransitionEntering()
        {

        }
        public void OnTransitionActive()
        {
            _eventService.Raise<GameplayUnloadSceneActivedEvent>();
        }

        public void OnTransitionExiting()
        {

        }
        public void OnTransitionInactive()
        {
            _sceneFlowService.UnloadGameplayUnloadScene();
        }

        public void OnEventInvoked(GameplayFinishEvent eventArg)
        {
            Animator.SetTrigger("Close");
            // trigger the close animation, and the rest of the process will be handled by the animation events
        }
    }
}
