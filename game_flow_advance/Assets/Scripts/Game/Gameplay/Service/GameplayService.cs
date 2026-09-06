using RossoGames.Inputs.Events;
using RossoGames.Level.Events;
using RossoGames.PopupFlow.Service;
using RossoGames.SceneFlow.Service;
using System;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    public class GameplayService : IGameplayService, IDisposable,
        //--INPUTS--
        IEventListener<CancelInputPressedEvent>,
        //--LEVEL--
        IEventListener<LevelLoadedEvent>
    {
        private IEventService _eventService;
        private IPopupFlowService _popupFlowService;
        private ISceneFlowService _sceneFlowService;
        private ITimeFlowService _timeFlowService;

        private GameplayDataService _serviceData;
        private GameplayStateMachine _stateMachine;

        public GameplayDataService ServiceData => _serviceData;

        public GameplayService(GameplayDataService serviceData)
        {
            _serviceData = serviceData;
        }
        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _popupFlowService = ServiceLocator.Get<IPopupFlowService>();
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();
            _timeFlowService = ServiceLocator.Get<ITimeFlowService>();

            _stateMachine = new GameplayStateMachine(_serviceData);
            _stateMachine.StartMachine(_stateMachine.PhaseStandBy);
        }
        public void Dispose()
        {
            UnregisterEvents();
        }

        // --TRANSITIONS--
        public Type GetCurrentPhase()
        {
            if (_stateMachine == null || _stateMachine.CurrentState == null)
                return null;

            return _stateMachine.CurrentState.GetType();
        }
        public Awaitable<bool> TransitionToPhaseStandBy()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseStandBy);
        }

        //--GAME FLOW--
        public async void StartGameplay()
        {
            RegisterEvents();
            await _stateMachine.TransitionTo(_stateMachine.PhaseLevelLoad);
            await _sceneFlowService.GoToGamePlayScene();
        }

        //--INPUTS--
        public void OnEventInvoked(CancelInputPressedEvent eventArg)
        {
            if (!CanInvokenInputEvent())
                return;

            if (!_stateMachine.CurrentState.DataBehaviour.AllowPause)
                return;

            _ = PauseGame();
        }

        //--LEVEL--
        public void OnEventInvoked(LevelLoadedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);

        private void RegisterEvents()
        {
            //--INPUTS--
            _eventService.RegisterListener<CancelInputPressedEvent>(this);
            //--LEVEL--
            _eventService.RegisterListener<LevelLoadedEvent>(this);
        }
        private void UnregisterEvents()
        {
            //--INPUTS--
            _eventService.UnregisterListener<CancelInputPressedEvent>(this);
            //--LEVEL--
            _eventService.UnregisterListener<LevelLoadedEvent>(this);
        }
        private bool CanInvokenInputEvent()
        {
            return
                !_timeFlowService.IsPaused &&
                _stateMachine != null &&
                _stateMachine.CurrentState != null &&
                !_stateMachine.IsTransitionInProgress;
        }
        private async Awaitable PauseGame()
        {
            _timeFlowService.PauseTimeFlow();
            var popupData = await _popupFlowService.OpenPause();

            if (popupData.ReturnToMain)
                await _stateMachine.TransitionTo(_stateMachine.PhaseLevelUnload);
            else
                _timeFlowService.ResumeTimeFlow();
        }
    }
}