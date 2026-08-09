using Rossoforge.Core.Events;
using Rossoforge.Services;
using Rossoforge.Utils.StateMachine;
using RossoGames.Cameras.Service;
using RossoGames.Currencies.Service;
using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.Events;
using RossoGames.Gameplay.Service;
using RossoGames.Inputs.Service;
using RossoGames.Level.Events;
using RossoGames.Level.Service;

namespace RossoGames.Gameplay.Phases
{
    public abstract class GameplayBasePhase : IState
    {
        protected IEventService _eventService;
        protected IInputsService _gameInputsService;
        protected ICameraService _cameraService;
        protected IGameplayService _gameplayService;
        protected ILevelService _levelService;
        protected ICurrencyService _currencyService;

        public bool IsPaused { get; private set; }
        public GameplayBasePhaseDataBehaviour DataBehaviour { get; private set; }

        public GameplayBasePhase(GameplayBasePhaseDataBehaviour dataBehaviour)
        {
            DataBehaviour = dataBehaviour;

            _eventService = ServiceLocator.Get<IEventService>();
            _gameInputsService = ServiceLocator.Get<IInputsService>();
            _cameraService = ServiceLocator.Get<ICameraService>();
            _gameplayService = ServiceLocator.Get<IGameplayService>();
            _levelService = ServiceLocator.Get<ILevelService>();
            _currencyService = ServiceLocator.Get<ICurrencyService>();
        }

        public virtual void Enter()
        {
            _eventService.Raise(new GameplayPhaseChangedEvent(this));
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
        }
        //--GAMEPLAY--
        public virtual void OnEventInvoked(GameplayUnloadSceneActivedEvent eventArg)
        {
        }
        public virtual void OnEventInvoked(GameplayFinishEvent eventArg)
        {
        }

        //--LEVEL--
        public virtual void OnEventInvoked(LevelLoadedEvent eventArg)
        {
        }
        public virtual void OnEventInvoked(LevelUnloadedEvent eventArg)
        {
        }
    }
}
