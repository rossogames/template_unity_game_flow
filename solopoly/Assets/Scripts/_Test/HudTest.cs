using Rossoforge.Core.Events;
using Rossoforge.Services;
using RossoGames.Gameplay.Events;
using RossoGames.Gameplay.Service;
using System;
using TMPro;
using UnityEngine;

namespace RossoGames.Test
{
    public class HudTest : MonoBehaviour,
        IEventListener<GameplayPhaseChangedEvent>
    {
        [SerializeField]
        private TextMeshProUGUI _label;

        private IEventService _eventService;
        private IGameplayService gameplayService;

        private Type _phaseType;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            gameplayService = ServiceLocator.Get<IGameplayService>();
        }

        private void Start()
        {
            _phaseType = gameplayService.GetCurrentPhase();
        }

        private void OnEnable()
        {
            _eventService.RegisterListener<GameplayPhaseChangedEvent>(this);
        }

        private void OnDisable()
        {
            _eventService.UnregisterListener<GameplayPhaseChangedEvent>(this);
        }

        private void Update()
        {
            string text = "";

            if (_phaseType != null)
                text = _phaseType.Name + "\r\n";

            _label.text = text;
        }

        public void OnEventInvoked(GameplayPhaseChangedEvent eventArg)
        {
            _phaseType = eventArg.CurrentPhase.GetType();
        }
    }
}
