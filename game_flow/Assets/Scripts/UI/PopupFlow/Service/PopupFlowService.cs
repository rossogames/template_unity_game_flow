using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Pool.DataConfig;
using Rossoforge.Pool.Service;
using Rossoforge.Popups.Service;
using Rossoforge.Popups.UI;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossoforge.Utils.Logger;
using Rossogames.Common;
using Rossogames.Inputs.Events;
using Rossogames.Popups.Pause;
using Rossogames.Popups.Question;
using Rossogames.Popups.Settings;
using System;
using UnityEngine;

namespace Rossogames.PopupFlow.Service
{
    public class PopupFlowService : IPopupFlowService, IInitializable, IDisposable,
        IEventListener<CancelInputPressedEvent>
    {
        private IEventService _eventService;
        private IPopupService _popupService;
        private PopupFlowDataService _dataService;

        public PopupFlowService(PopupFlowDataService dataService)
        {
            _dataService = dataService;
        }

        public void Initialize()
        {
            if (_dataService == null)
            {
                RossoLogger.Error($"{nameof(PopupFlowDataService)} not assigned");
                return;
            }

            _eventService = ServiceLocator.Get<IEventService>();
            _popupService = ServiceLocator.Get<IPopupService>();

            _eventService.RegisterListener<CancelInputPressedEvent>(this);
        }

        public void Dispose()
        {
            _eventService.UnregisterListener<CancelInputPressedEvent>(this);
        }

        public async Awaitable<QuestionResult> OpenConfirmQuit()
        {
            // SAMPLE:
            // Replace these strings with your localization system (e.g., ILocalization, tables, etc.)
            // or pass them as parameters if you prefer.
            var popupData = new PopupQuestionData
            {
                Title = "Exit Game?",
                Message = "Systems are ready.\r\nGameplay is yours",
                ConfirmButtonText = "Yes",
                CancelButtonText = "No"
            };

            await OpenPopupUntilClosed<PopupQuestionView, PopupQuestionData>(
                _dataService.PopupQuestionAssetReference,
                popupData,
                poolCategory: PoolCategories.MainUI
            );

            return popupData.Result;
        }

        public void OpenSettings()
        {
            _ = OpenPopup<PopupSettingsView, IPopupData>(
                _dataService.PopupSettingsAssetReference,
                null,
                poolCategory: PoolCategories.MainUI
            );
        }

        public async Awaitable<PopupPauseData> OpenPause()
        {
            var popupData = new PopupPauseData();

            await OpenPopupUntilClosed<PopupPauseView, PopupPauseData>(
                _dataService.PopupPauseAssetReference,
                popupData,
                poolCategory: PoolCategories.Gameplay
            );

            return popupData;
        }

        private async Awaitable<TView> OpenPopupUntilClosed<TView, TData>(
            PooledGameobjectDataConfig assetReference,
            TData popupData,
            Vector3 position = default,
            Space relativeTo = Space.Self,
            string poolCategory = IPoolService.DEFAULT_CATEGORY
        )
                where TView : MonoBehaviour, IPopupView
                where TData : IPopupData
        {
            if (assetReference == null)
                return null;

            return await _popupService.OpenPopupUntilClosed<TView>(assetReference, popupData, position, relativeTo, poolCategory);
        }

        private TView OpenPopup<TView, TData>(
            PooledGameobjectDataConfig assetReference,
            TData popupData,
            Vector3 position = default,
            Space relativeTo = Space.Self,
            string poolCategory = IPoolService.DEFAULT_CATEGORY
        )
        where TView : MonoBehaviour, IPopupView
        where TData : IPopupData
        {
            if (assetReference == null)
                return null;

            return _popupService.OpenPopup<TView>(assetReference, popupData, position, relativeTo, poolCategory);
        }

        public void OnEventInvoked(CancelInputPressedEvent eventArg)
        {
            _popupService.CancelPopup();
        }
    }
}
