using Rossoforge.Core.Events;
using Rossoforge.Core.TimeFlow;
using Rossoforge.Services;
using RossoGames.Buildings.DataAssets;
using RossoGames.Buildings.DataEntities;
using RossoGames.Cards.Events;
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
        IEventListener<GameplayUnloadSceneActivedEvent>,
        IEventListener<GameplayFinishEvent>,
        IEventListener<GameplayRollDiceStartedEvent>,
        IEventListener<GameplayRollDiceEndedEvent>,
        IEventListener<GameplayTokenLandedEvent>,
        IEventListener<BuildingCardClickedEvent>,
        IEventListener<GameplayCardSelectionSkippedEvent>,
        //--LEVEL--
        IEventListener<LevelLoadedEvent>,
        IEventListener<LevelUnloadedEvent>
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
        public Awaitable<bool> TransitionToPhaseDiceRoll()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseDices);
        }
        public Awaitable<bool> TransitionToPhaseTokenMovement()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseTokenMovement);
        }
        public Awaitable<bool> TransitionToPhaseTransportSelection()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseTransportSelection);
        }
        public Awaitable<bool> TransitionToPhaseCardSelection()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhaseCardSelection);
        }
        public Awaitable<bool> TransitionToPhasePhaseBuilding()
        {
            return _stateMachine.TransitionTo(_stateMachine.PhasePhaseBuilding);
        }

        // --INITIALIZE PHASES--
        public void InitializePhaseBuilding(BuildingDataEntity dataEntity)
        {
            _stateMachine.PhasePhaseBuilding.Initialize(dataEntity);
        }

        //--GAME FLOW--
        public async void StartGameplay()
        {
            RegisterEvents();
            await _stateMachine.TransitionTo(_stateMachine.PhaseLevelLoad);
            _sceneFlowService.GoToGamePlayScene();
        }
        public void FinishGameplay()
        {
            _sceneFlowService.UnloadGameplayScene();
            _sceneFlowService.LoadMainScene();
            _timeFlowService.ResumeTimeFlow();

            _eventService.Raise<GameplayFinishEvent>();

            UnregisterEvents();
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
        public void OnEventInvoked(GameplayUnloadSceneActivedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(GameplayFinishEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(GameplayRollDiceStartedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(GameplayRollDiceEndedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(GameplayTokenLandedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(BuildingCardClickedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(GameplayCardSelectionSkippedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);

        //--LEVEL--
        public void OnEventInvoked(LevelLoadedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);
        public void OnEventInvoked(LevelUnloadedEvent eventArg) => _stateMachine.CurrentState.OnEventInvoked(eventArg);

        private void RegisterEvents()
        {
            //--INPUTS--
            _eventService.RegisterListener<CancelInputPressedEvent>(this);
            //--GAMEPLAY--
            _eventService.RegisterListener<GameplayUnloadSceneActivedEvent>(this);
            _eventService.RegisterListener<GameplayFinishEvent>(this);
            _eventService.RegisterListener<GameplayRollDiceStartedEvent>(this);
            _eventService.RegisterListener<GameplayRollDiceEndedEvent>(this);
            _eventService.RegisterListener<GameplayTokenLandedEvent>(this);
            _eventService.RegisterListener<BuildingCardClickedEvent>(this);
            _eventService.RegisterListener<GameplayCardSelectionSkippedEvent>(this);
            //--LEVEL--
            _eventService.RegisterListener<LevelLoadedEvent>(this);
            _eventService.RegisterListener<LevelUnloadedEvent>(this);
        }
        private void UnregisterEvents()
        {
            //--INPUTS--
            _eventService.UnregisterListener<CancelInputPressedEvent>(this);
            //--GAMEPLAY--
            _eventService.UnregisterListener<GameplayUnloadSceneActivedEvent>(this);
            _eventService.UnregisterListener<GameplayFinishEvent>(this);
            _eventService.UnregisterListener<GameplayRollDiceStartedEvent>(this);
            _eventService.UnregisterListener<GameplayRollDiceEndedEvent>(this);
            _eventService.UnregisterListener<GameplayTokenLandedEvent>(this);
            _eventService.UnregisterListener<BuildingCardClickedEvent>(this);
            _eventService.UnregisterListener<GameplayCardSelectionSkippedEvent>(this);
            //--LEVEL--
            _eventService.UnregisterListener<LevelLoadedEvent>(this);
            _eventService.UnregisterListener<LevelUnloadedEvent>(this);
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
                UnloadGameplay();
            else
                _timeFlowService.ResumeTimeFlow();
        }
        private async void UnloadGameplay()
        {
            await _stateMachine.TransitionTo(_stateMachine.PhaseLevelUnload);
            _sceneFlowService.LoadGameplayUnloadScene(); // blackout the screen while the level is unloading
        }
    }
}