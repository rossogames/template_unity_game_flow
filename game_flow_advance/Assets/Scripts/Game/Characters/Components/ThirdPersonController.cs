using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.TimeFlow.Events;
using Rossogames.Characters.DataBehavior;
using Rossogames.Characters.DataCache;
using Rossogames.Characters.DataRuntime;
using Rossogames.Characters.States;
using Rossogames.LevelObjects.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Characters.Components
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Highlightable))]
    public class ThirdPersonController : MonoBehaviour,
        IEventListener<TimeFlowPausedEvent>,
        IEventListener<TimeFlowResumedEvent>
    {
        [field: SerializeField]
        public CharacterDataBehaviour DataBehaviour { get; private set; }

        [SerializeField, BoxGroup("Audio")]
        private AudioSource _audioFootsteps;
        [SerializeField, BoxGroup("Audio")]
        private AudioSource _audioLanding;

        private Animator _animator;
        private CharacterController _controller;
        private Highlightable _highlightable;

        private IEventService _eventService;

        public CharacterStateMachine StateMachine { get; private set; }
        public CharacterAnimationDataCache AnimationCache { get; private set; }
        public CharacterDataRuntime Runtime { get; private set; }
        public Vector3 Velocity => _controller.velocity;


        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();

            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
            _highlightable = GetComponent<Highlightable>();

            _eventService.RegisterListener<TimeFlowPausedEvent>(this);
            _eventService.RegisterListener<TimeFlowResumedEvent>(this);
        }
        private void Start()
        {
            AnimationCache = new();
            Runtime = new(DataBehaviour);

            StateMachine = new CharacterStateMachine(this);
            StateMachine.StartMachine(StateMachine.Idle);

            _highlightable.Show();
        }
        private void OnDestroy()
        {
            _eventService.UnregisterListener<TimeFlowPausedEvent>(this);
            _eventService.UnregisterListener<TimeFlowResumedEvent>(this);
        }

        private void Update()
        {
            StateMachine.Update();
        }

        public void Move(Vector3 motion)
        {
            _controller.Move(motion);
        }
        public void SetAnimation(int clipId, float value)
        {
            _animator.SetFloat(clipId, value);
        }
        public void SetAnimation(int clipId, bool value)
        {
            _animator.SetBool(clipId, value);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            _audioFootsteps.Play();
        }
        private void OnLand(AnimationEvent animationEvent)
        {
            _audioLanding.Play();
        }

        public void OnEventInvoked(TimeFlowPausedEvent eventArg)
        {
            enabled = false;
        }
        public void OnEventInvoked(TimeFlowResumedEvent eventArg)
        {
            enabled = true;
        }
    }
}