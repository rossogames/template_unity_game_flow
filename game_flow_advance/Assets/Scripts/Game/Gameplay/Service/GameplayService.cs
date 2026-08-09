using Rossoforge.Core.Events;
using Rossoforge.Core.TimeFlow;
using Rossoforge.Services;
using RossoGames.Gameplay.Events;
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
        //--GAMEPLAY--
        IEventListener<GameplayFinishEvent>,
        //--LEVEL--
        IEventListener<LevelLoadedEvent>
    {
        private IEventService _eventService;
        private IPopupFlowService _popupFlowService;
        private ISceneFlowService _sceneFlowService;
        private ITimeFlowService _timeFlowService;

        private GameplayServiceData _serviceData;
        private GameplayStateMachine _stateMachine;

        public GameplayServiceData ServiceData => _serviceData;

        public GameplayService(GameplayServiceData serviceData)
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
            if (_stateMachine == null || _stateMachine.CurrentState == null)
                return;

            if (_timeFlowService.IsPaused)
                return;

            if (!_stateMachine.CurrentState.DataBehaviour.AllowPause)
                return;

            _ = PauseGame();
        }

        //--GAMEPLAY--
        public void OnEventInvoked(GameplayFinishEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);

        //--LEVEL--
        public void OnEventInvoked(LevelLoadedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);

        private void RegisterEvents()
        {
            //--INPUTS--
            _eventService.RegisterListener<CancelInputPressedEvent>(this);
            //--GAMEPLAY--
            _eventService.RegisterListener<GameplayFinishEvent>(this);
            //--LEVEL--
            _eventService.RegisterListener<LevelLoadedEvent>(this);
        }
        private void UnregisterEvents()
        {
            //--INPUTS--
            _eventService.UnregisterListener<CancelInputPressedEvent>(this);
            //--GAMEPLAY--
            _eventService.UnregisterListener<GameplayFinishEvent>(this);
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