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
using Rossogames.Inventory.Service;
using Rossogames.Level.Service;
using Rossogames.PopupFlow.Service;
using Rossogames.Progression.Service;
using Rossogames.Raycast.Service;
using Rossogames.SceneFlow.Service;
using Rossogames.Settings.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Boot.Components
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
        [SerializeField, BoxGroup("Game")] private InventoryDataService _inventoryDataService;

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
            var raycastService = new RaycastService();
            var progressionDataService = new ProgressionService(_progressionDataService);

            ServiceLocator.Register<IInputsService>(inptuService);
            ServiceLocator.Register<ISceneFlowService>(sceneFlowService);
            ServiceLocator.Register<IPopupFlowService>(popupFlowService);
            ServiceLocator.Register<ISettingsService>(settingsService);
            ServiceLocator.Register<IRaycastService>(raycastService);
            ServiceLocator.Register<IProgressionService>(progressionDataService);
        }

        private void RegisterGameServices()
        {
            var cameraService = new CameraService(_cameraDataService);
            var gameplayService = new GameplayService(_gameplayDataService);
            var levelService = new LevelService(_levelDataService);
            var currencyService = new CurrencyService();
            var inventoryService = new InventoryService(_inventoryDataService);

            ServiceLocator.Register<ICameraService>(cameraService);
            ServiceLocator.Register<IGameplayService>(gameplayService);
            ServiceLocator.Register<ILevelService>(levelService);
            ServiceLocator.Register<ICurrencyService>(currencyService);
            ServiceLocator.Register<IInventoryService>(inventoryService);
        }
    }
}