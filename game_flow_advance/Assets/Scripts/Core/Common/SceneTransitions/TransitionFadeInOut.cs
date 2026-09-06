using UnityEngine;

namespace RossoGames.Common.SceneTransitions
{
    [RequireComponent(typeof(Animator))]
    public class TransitionFadeInOut : SceneTransition
    {
        [HideInInspector] public Animator Animator;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            Animator = GetComponent<Animator>();
        }

        override protected void OnTargetSceneLoadedCompletedEvent()
        {
            Animator.SetTrigger("Close");
        }
    }
}