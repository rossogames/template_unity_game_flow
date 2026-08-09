using Rossoforge.Audio.Services;
using Rossoforge.Core.Audio;
using Rossoforge.Core.Events;
using Rossoforge.Core.Pool;
using Rossoforge.Core.Scenes;
using Rossoforge.Core.TimeFlow;
using Rossoforge.Core.UI.Popups;
using Rossoforge.Core.UserData;
using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Scenes.Service;
using Rossoforge.Services;
using Rossoforge.TimeFlow.Service;
using Rossoforge.UI.Popups.Service;
using Rossoforge.UserData.Service;
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
        [SerializeField, BoxGroup("SDK")] private SceneServiceData _sceneServiceData;
        [SerializeField, BoxGroup("SDK")] private PopupServiceData _popupServiceData;
        [SerializeField, BoxGroup("SDK")] private AudioServiceData _audioServiceData;
        [SerializeField, BoxGroup("SDK")] private UserDataServiceData _userDataServiceData;

        [SerializeField, BoxGroup("Core")] private SettingsServiceData _settingsServiceData;
        [SerializeField, BoxGroup("Core")] private SceneFlowServiceData _sceneFlowServiceData;
        [SerializeField, BoxGroup("Core")] private PopupFlowServiceData _popupFlowServiceData;
        [SerializeField, BoxGroup("Core")] private ProgressionServiceData _progressionServiceData;

        [SerializeField, BoxGroup("Game")] private CameraServiceData _cameraServiceData;
        [SerializeField, BoxGroup("Game")] private GameplayServiceData _gameplayServiceData;
        [SerializeField, BoxGroup("Game")] private LevelServiceData _levelServiceData;

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
            var sceneService = new SceneService(_sceneServiceData);
            var poolService = new PoolService();
            var popupService = new PopupService(_popupServiceData);
            var audioService = new AudioService(_audioServiceData);
            var timeFlowService = new TimeFlowService();
            var userDataService = new UserDataService<SaveData>(_userDataServiceData);

            ServiceLocator.Register<IEventService>(eventService);
            ServiceLocator.Register<ISceneService>(sceneService);
            ServiceLocator.Register<IPoolService>(poolService);
            ServiceLocator.Register<IPopupService>(popupService);
            ServiceLocator.Register<IAudioService>(audioService);
            ServiceLocator.Register<ITimeFlowService>(timeFlowService);
            ServiceLocator.Register<IUserDataService<SaveData>>(userDataService);
        }

        private void RegisterCoreServices()
        {
            var inptuService = new InputsService();
            var sceneFlowService = new SceneFlowService(_sceneFlowServiceData);
            var popupFlowService = new PopupFlowService(_popupFlowServiceData);
            var settingsService = new SettingsService(_settingsServiceData);
            var raycastService = new RaycastService();
            var progressionDataService = new ProgressionService(_progressionServiceData);

            ServiceLocator.Register<IInputsService>(inptuService);
            ServiceLocator.Register<ISceneFlowService>(sceneFlowService);
            ServiceLocator.Register<IPopupFlowService>(popupFlowService);
            ServiceLocator.Register<ISettingsService>(settingsService);
            ServiceLocator.Register<IRaycastService>(raycastService);
            ServiceLocator.Register<IProgressionService>(progressionDataService);
        }

        private void RegisterGameServices()
        {
            var cameraService = new CameraService(_cameraServiceData);
            var gameplayService = new GameplayService(_gameplayServiceData);
            var levelService = new LevelService(_levelServiceData);
            var currencyService = new CurrencyService();

            ServiceLocator.Register<ICameraService>(cameraService);
            ServiceLocator.Register<IGameplayService>(gameplayService);
            ServiceLocator.Register<ILevelService>(levelService);
            ServiceLocator.Register<ICurrencyService>(currencyService);
        }
    }
}