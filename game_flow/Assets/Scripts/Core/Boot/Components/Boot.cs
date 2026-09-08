using Rossoforge.Audio.Service;
using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Popups.Service;
using Rossoforge.Scenes.Service;
using Rossoforge.Services.Locator;
using Rossoforge.TimeFlow.Service;
using Rossogames.Cameras.Service;
using Rossogames.Currencies.Service;
using Rossogames.Gameplay.Service;
using Rossogames.Inputs.Service;
using Rossogames.Level.Service;
using Rossogames.PopupFlow.Service;
using Rossogames.Progression.Service;
using Rossogames.SceneFlow.Service;
using Rossogames.Settings.Service;
using UnityEngine;

namespace Rossogames.Boot.Components
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private SceneDataService _sceneDataService;
        [SerializeField] private PopupDataService _popupDataService;
        [SerializeField] private AudioDataService _audioDataService;

        [Space]
        [SerializeField] private SettingsDataService _settingsDataService;
        [SerializeField] private SceneFlowDataService _sceneFlowDataService;
        [SerializeField] private PopupFlowDataService _popupFlowDataService;
        [SerializeField] private ProgressionDataService _progressionDataService;

        [Space]
        [SerializeField] private CameraDataService _cameraDataService;
        [SerializeField] private GameplayDataService _gameplayDataService;
        [SerializeField] private LevelDataService _levelDataService;

        private void Awake()
        {
            // Setup
            ServiceLocator.SetLocator(new DefaultServiceLocator());

            RegisterSdkServices();
            RegisterCoreServices();
            RegisterGameServices();

            ServiceLocator.Initialize();
        }
        private void Start()
        {
            ServiceLocator.Get<ISceneFlowService>().GoToMainScene();
        }

        private void RegisterSdkServices()
        {
            var eventService = new EventService();
            var sceneService = new SceneService(_sceneDataService);
            var poolService = new PoolService();
            var popupService = new PopupService(_popupDataService);
            var audioService = new AudioService(_audioDataService);
            var timeFlowService = new TimeFlowService();

            ServiceLocator.Register<IEventService>(eventService);
            ServiceLocator.Register<ISceneService>(sceneService);
            ServiceLocator.Register<IPoolService>(poolService);
            ServiceLocator.Register<IPopupService>(popupService);
            ServiceLocator.Register<IAudioService>(audioService);
            ServiceLocator.Register<ITimeFlowService>(timeFlowService);
        }

        private void RegisterCoreServices()
        {
            var inptuService = new InputsService();
            var sceneFlowService = new SceneFlowService(_sceneFlowDataService);
            var popupFlowService = new PopupFlowService(_popupFlowDataService);
            var settingsService = new SettingsService(_settingsDataService);
            var progressionDataService = new ProgressionService(_progressionDataService);

            ServiceLocator.Register<IInputsService>(inptuService);
            ServiceLocator.Register<ISceneFlowService>(sceneFlowService);
            ServiceLocator.Register<IPopupFlowService>(popupFlowService);
            ServiceLocator.Register<ISettingsService>(settingsService);
            ServiceLocator.Register<IProgressionService>(progressionDataService);
        }

        private void RegisterGameServices()
        {
            var cameraService = new CameraService(_cameraDataService);
            var gameplayService = new GameplayService(_gameplayDataService);
            var levelService = new LevelService(_levelDataService);
            var currencyService = new CurrencyService();

            ServiceLocator.Register<ICameraService>(cameraService);
            ServiceLocator.Register<IGameplayService>(gameplayService);
            ServiceLocator.Register<ILevelService>(levelService);
            ServiceLocator.Register<ICurrencyService>(currencyService);
        }
    }
}