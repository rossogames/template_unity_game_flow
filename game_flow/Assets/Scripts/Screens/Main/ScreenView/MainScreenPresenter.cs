using Rossogames.Popups.Question;
using Rossogames.PopupFlow.Service;
using Rossogames.Gameplay.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Screens.UI;
#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif

namespace Rossogames.Main.ScreenView
{
    public class MainScreenPresenter : ScreenPresenter<MainScreenView, MainScreenPresenter>
    {
        private readonly IPopupFlowService _popupFlowService;
        private readonly IGameplayService _gameplayService;

        public MainScreenPresenter(MainScreenView view) : base(view)
        {
            _popupFlowService = ServiceLocator.Get<IPopupFlowService>();
            _gameplayService = ServiceLocator.Get<IGameplayService>();
        }

        public void PlayGame()
        {
            _gameplayService.StartGameplay();
        }
        public void OpenSettingsPopup()
        {
            _popupFlowService.OpenSettings();
        }
        public async void OpenExitConfirmationPopup()
        {
            var result = await _popupFlowService.OpenConfirmQuit();
            if (result == QuestionResult.Ok)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}