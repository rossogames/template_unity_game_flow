using Rossoforge.Audio.Service;
using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Popups.Service;
using Rossoforge.Scenes.Service;
using Rossoforge.Services.Locator;
using Rossoforge.TimeFlow.Service;
using RossoGames.Cameras.Service;
using RossoGames.Currencies.Service;
using RossoGames.Gameplay.Service;
using RossoGames.Inputs.Service;
using RossoGames.Level.Service;
using RossoGames.PopupFlow.Service;
using RossoGames.Progression.Data;
using RossoGames.Progression.Service;
using RossoGames.Raycast.Service;
using RossoGames.SceneFlow.Service;
using RossoGames.Settings.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Boot.Components
{
    public class Boot : MonoBehaviour
    {
        [SerializeField, BoxGroup("SDK")] private SceneDataService _sceneDataService;
        [SerializeField, BoxGroup("SDK")] private PopupDataService _popupDataService;
        [SerializeField, BoxGroup("SDK")] private AudioDataService _audioDataService;

        [SerializeField, BoxGroup("Core")] private SettingsDataService _settingsDataService;
        [SerializeField, BoxGroup("Core")] private SceneFlowDataService _sceneFlowDataService;
        [SerializeField, BoxGroup("Core")] private PopupFlowDataService _popupFlowDataService;
        [SerializeField, BoxGroup("Core")] private ProgressionDataService _progressionDataService;

        [SerializeField, BoxGroup("Game")] private CameraDataService _cameraDataService;
        [SerializeField, BoxGroup("Game")] private GameplayDataService _gameplayDataService;
        [SerializeField, BoxGroup("Game")] private LevelDataService _levelDataService;

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