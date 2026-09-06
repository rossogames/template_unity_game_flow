using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Pool.DataConfig;
using Rossoforge.Popups.Service;
using Rossoforge.Popups.UI;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossoforge.Utils.Logger;
using RossoGames.Inputs.Events;
using RossoGames.Popups.PopupPause;
using RossoGames.Popups.PopupQuestion;
using RossoGames.Popups.PopupSettings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RossoGames.PopupFlow.Service
{
    public class PopupFlowService : IPopupFlowService, IInitializable, IDisposable,
        IEventListener<CancelInputPressedEvent>
    {
        private IEventService _eventService;
        private IPopupService _popupService;
        private PopupFlowDataService _serviceData;

        private Dictionary<PopupType, PooledGameobjectDataConfig> _popupsMap = new();

        public PopupFlowService(PopupFlowDataService serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
            if (_serviceData == null)
            {
                RossoLogger.Error($"{nameof(PopupFlowDataService)} not assigned");
                return;
            }

            _eventService = ServiceLocator.Get<IEventService>();
            _popupService = ServiceLocator.Get<IPopupService>();

            _eventService.RegisterListener<CancelInputPressedEvent>(this);

            InitializePopupsMapper();
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
                 PopupType.ConfirmQuit,
                 popupData);

            return popupData.Result;
        }

        public void OpenSettings()
        {
            _ = OpenPopup<PopupSettingsView, IPopupData>(
                 PopupType.Settings,
                 null);
        }

        public async Awaitable<PopupPauseData> OpenPause()
        {
            var popupData = new PopupPauseData();

            await OpenPopupUntilClosed<PopupPauseView, PopupPauseData>(
                 PopupType.Pause,
                 popupData);

            return popupData;
        }

        private async Awaitable<TView> OpenPopupUntilClosed<TView, TData>(
            PopupType popupType,
            TData popupData,
            Vector3 position = default,
            Space relativeTo = Space.Self)
                where TView : MonoBehaviour, IPopupView
                where TData : IPopupData
        {
            var assetReference = GetPooledPopupReference(popupType);
            if (assetReference == null)
                return null;

            return await _popupService.OpenPopupUntilClosed<TView>(assetReference, popupData, position, relativeTo);
        }

        private TView OpenPopup<TView, TData>(
            PopupType popupType,
            TData popupData,
            Vector3 position = default,
            Space relativeTo = Space.Self)
        where TView : MonoBehaviour, IPopupView
        where TData : IPopupData
        {
            var assetReference = GetPooledPopupReference(popupType);
            if (assetReference == null)
                return null;

            return _popupService.OpenPopup<TView>(assetReference, popupData, position, relativeTo);
        }

        private PooledGameobjectDataConfig GetPooledPopupReference(PopupType popupType)
        {
            if (!_popupsMap.TryGetValue(popupType, out PooledGameobjectDataConfig assetReference))
            {
                RossoLogger.Error($"Popup asset for type '{popupType}' not found. Check {nameof(PopupFlowDataService)}");
                return null;
            }

            return assetReference;
        }

        private void InitializePopupsMapper()
        {
            _popupsMap.Clear();

            if (_serviceData.Popups == null || _serviceData.Popups.Length == 0)
            {
                RossoLogger.Warning($"No popups configured in {nameof(PopupFlowDataService)}");
                return;
            }

            foreach (var entry in _serviceData.Popups)
            {
                if (_popupsMap.ContainsKey(entry.Type))
                {
                    RossoLogger.Warning($"Duplicate PopupType entry: {entry.Type}");
                    continue;
                }

                if (entry.AssetReference == null)
                {
                    RossoLogger.Warning($"Popup '{entry.Type}' has no AssetReference assigned");
                    continue;
                }

                _popupsMap.Add(entry.Type, entry.AssetReference);
            }
        }

        public void OnEventInvoked(CancelInputPressedEvent eventArg)
        {
            _popupService.CancelPopup();
        }
    }
}
