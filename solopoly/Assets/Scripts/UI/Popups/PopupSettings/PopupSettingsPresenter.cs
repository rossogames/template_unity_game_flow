using Rossoforge.Core.UI.Popups;
using Rossoforge.Services;
using Rossoforge.UI.Popups.PopupBase;
using RossoGames.Settings.Service;

namespace RossoGames.Popups.PopupSettings
{
    public class PopupSettingsPresenter : PopupPresenter<PopupSettingsView, PopupSettingsPresenter, IPopupData>
    {
        private ISettingsService _settingsService;

        public PopupSettingsPresenter(PopupSettingsView view) : base(view)
        {
            _settingsService = ServiceLocator.Get<ISettingsService>();
        }

        public override void OnOpening()
        {
            base.OnOpening();
            SetView();
        }

        public void SetMusicEnabled(bool value)
        {
            _settingsService.MusicEnabled = value;
            View.MusicVolumeSlider.interactable = value;
        }
        public void SetMusicVolume(float value)
        {
            _settingsService.MusicVolume = value;
        }
        public void SetSfxEnabled(bool value)
        {
            _settingsService.SfxEnabled = value;
            View.SfxVolumeSlider.interactable = value;
        }
        public void SetSfxVolume(float value)
        {
            _settingsService.SfxVolume = value;
        }

        public void SaveSettings()
        {
            _settingsService.Save();
            View.Close();
        }

        public void CancelSettings()
        {
            _settingsService.Load();
            View.Close();
        }

        public void SetView()
        {
            View.MusicEnabledSwitch.IsOn = _settingsService.MusicEnabled;
            View.MusicVolumeSlider.value = _settingsService.MusicVolume;

            View.SfxEnabledSwitch.IsOn = _settingsService.SfxEnabled;
            View.SfxVolumeSlider.value = _settingsService.SfxVolume;
        }
    }
}
