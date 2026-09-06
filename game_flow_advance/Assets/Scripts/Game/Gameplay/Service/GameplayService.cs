using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossoforge.TimeFlow.Service;
using Rossogames.Inputs.Events;
using Rossogames.Level.Events;
using Rossogames.PopupFlow.Service;
using Rossogames.SceneFlow.Service;
using System;
using UnityEngine;

namespace Rossogames.Gameplay.Service
{
    public class GameplayService : IGameplayService, IInitializable, IDisposable,
        //--INPUTS--
        IEventListener<PauseInputPressedEvent>,
        //--LEVEL--
        IEventListener<LevelLoadedEvent>
    {
        private IEventService _eventService;
        private IPopupFlowService _popupFlowService;
        private ISceneFlowService _sceneFlowService;
        private ITimeFlowService _timeFlowService;

        private GameplayDataService _dataService;
        private GameplayStateMachine _stateMachine;

        public GameplayDataService ServiceData => _dataService;

        public GameplayService(GameplayDataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _popupFlowService = ServiceLocator.Get<IPopupFlowService>();
            _sceneFlowService = ServiceLocator.Get<ISceneFlowService>();
            _timeFlowService = ServiceLocator.Get<ITimeFlowService>();

            _stateMachine = new GameplayStateMachine(_dataService);
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
        public Awaitable<bool> TransitionToPhaseExploration()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseExploration);
        }

        //--GAME FLOW--
        public async void StartGameplay()
        {
            RegisterEvents();
            await _stateMachine.TransitionTo(_stateMachine.PhaseLevelLoad);
            await _sceneFlowService.GoToGamePlayScene();
        }

        //--INPUTS--
        public void OnEventInvoked(PauseInputPressedEvent eventArg)
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

            _eventService.RegisterListener<PauseInputPressedEvent>(this);
            //--LEVEL--
            _eventService.RegisterListener<LevelLoadedEvent>(this);
        }
        private void UnregisterEvents()
        {
            //--INPUTS--
            _eventService.UnregisterListener<PauseInputPressedEvent>(this);
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