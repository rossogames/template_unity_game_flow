using Rossoforge.Core.Events;
using Rossoforge.Services;
using Rossoforge.UI.Controls.Buttons;
using RossoGames.Gameplay.Events;
using RossoGames.Gameplay.Phases.CardSelection;
using UnityEngine;
using UnityEngine.UI;

namespace RossoGames.Gameplay.Components
{
    public class HudCards : MonoBehaviour,
        IButtonClickListener<SkipButtonHandler>,
        IEventListener<GameplayPhaseChangedEvent>
    {
        [SerializeField]
        private Button _buttonSkip;

        private IEventService _eventService;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
        }

        private void OnEnable()
        {
            _eventService.RegisterListener<GameplayPhaseChangedEvent>(this);
        }

        private void OnDisable()
        {
            _eventService.UnregisterListener<GameplayPhaseChangedEvent>(this);
        }

        public void OnClick(ButtonEventArg<SkipButtonHandler> eventArg)
        {
            _eventService.Raise<GameplayCardSelectionSkippedEvent>();
        }

        public void OnEventInvoked(GameplayPhaseChangedEvent eventArg)
        {
            _buttonSkip.gameObject.SetActive(eventArg.CurrentPhase is PhaseCardSelection);
        }
    }
}
