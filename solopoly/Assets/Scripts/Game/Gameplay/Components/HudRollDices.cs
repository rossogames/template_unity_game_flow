using Rossoforge.Core.Events;
using Rossoforge.Services;
using RossoGames.Gameplay.Events;
using RossoGames.Gameplay.Phases.DiceRoll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RossoGames.Gameplay.Components
{
    public class HudRollDices : MonoBehaviour,
        IEventListener<GameplayPhaseChangedEvent>,
        IEventListener<GameplayRollDiceEndedEvent>
    {
        [SerializeField]
        private Button _buttonRollDices;

        [SerializeField]
        private TextMeshProUGUI _labelDiceValue;

        private IEventService _eventService;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
        }

        private void OnEnable()
        {
            _buttonRollDices.onClick.AddListener(OnButtonRollDicesClicked); // REFACTORIZAR POR UN IBUTTON CLICK LISTENER
            _eventService.RegisterListener<GameplayPhaseChangedEvent>(this);
            _eventService.RegisterListener<GameplayRollDiceEndedEvent>(this);
        }

        private void OnDisable()
        {
            _buttonRollDices.onClick.RemoveListener(OnButtonRollDicesClicked);
            _eventService.UnregisterListener<GameplayPhaseChangedEvent>(this);
            _eventService.UnregisterListener<GameplayRollDiceEndedEvent>(this);
        }

        private void OnButtonRollDicesClicked()
        {
            _eventService.Raise<GameplayRollDiceStartedEvent>();
        }

        private void SetDiceValue(int value)
        {
            _labelDiceValue.gameObject.SetActive(value > 0);
            _labelDiceValue.text = value.ToString();
        }

        public void OnEventInvoked(GameplayPhaseChangedEvent eventArg)
        {
            _buttonRollDices.gameObject.SetActive(eventArg.CurrentPhase is PhaseDiceRoll);
        }

        public void OnEventInvoked(GameplayRollDiceEndedEvent eventArg)
        {
            SetDiceValue(eventArg.DiceValue);
        }
    }
}
