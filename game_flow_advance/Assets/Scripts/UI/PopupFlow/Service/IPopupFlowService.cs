using Rossoforge.Services.Service;
using RossoGames.Popups.PopupPause;
using RossoGames.Popups.PopupQuestion;
using UnityEngine;

namespace RossoGames.PopupFlow.Service
{
    public interface IPopupFlowService : IService
    {
        Awaitable<QuestionResult> OpenConfirmQuit();
        void OpenSettings();
        Awaitable<PopupPauseData> OpenPause();
    }
}
