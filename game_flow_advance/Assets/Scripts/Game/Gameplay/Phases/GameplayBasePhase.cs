using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Utils.StateMachine;
using Rossogames.Cameras.Service;
using Rossogames.Currencies.Service;
using Rossogames.Gameplay.DataBehaviour;
using Rossogames.Gameplay.Events;
using Rossogames.Gameplay.Service;
using Rossogames.Inputs.Service;
using Rossogames.Level.Events;
using Rossogames.Level.Service;

namespace Rossogames.Gameplay.Phases
{
    public abstract class GameplayBasePhase : IState
    {
        protected IEventService _eventService;
        protected IInputsService _inputsService;
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
            _inputsService = ServiceLocator.Get<IInputsService>();
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

        //--LEVEL--
        public virtual void OnEventInvoked(LevelLoadedEvent eventArg)
        {
        }
    }
}
