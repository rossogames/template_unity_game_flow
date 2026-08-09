using Rossoforge.Core.UI.Popups;
using Rossoforge.UI.Controls.Buttons;
using Rossoforge.UI.Controls.Sliders;
using Rossoforge.UI.Controls.Switches;
using Rossoforge.UI.Popups.PopupBase;
using UnityEngine;
using UnityEngine.UI;

namespace RossoGames.Popups.PopupSettings
{
    public class PopupSettingsView : PopupView<PopupSettingsView, PopupSettingsPresenter, IPopupData>,
        ISliderValueChangedListener<SliderHandlerMusicVolume>,
        ISliderValueChangedListener<SliderHandlerSfxVolume>,
        ISwitchValueChangedListener<SwitchHandlerMusicEnabled>,
        ISwitchValueChangedListener<SwitchHandlerSfxEnabled>,
        IButtonClickListener<ButtonHandlerSave>
    {
        [SerializeField] private SwitchHandlerMusicEnabled _musicEnabled;
        [SerializeField] private SliderHandlerMusicVolume _musicVolume;

        [SerializeField] private SwitchHandlerSfxEnabled _sfxEnabled;
        [SerializeField] private SliderHandlerSfxVolume _sfxVolume;

        public Switch MusicEnabledSwitch => _musicEnabled.Switch;
        public Slider MusicVolumeSlider => _musicVolume.Slider;
        public Switch SfxEnabledSwitch => _sfxEnabled.Switch;
        public Slider SfxVolumeSlider => _sfxVolume.Slider;

        protected override void Awake()
        {
            base.Awake();
            Presenter = new PopupSettingsPresenter(this);
        }

        public void OnValueChanged(SwitchEventArg<SwitchHandlerMusicEnabled> eventArg) => Presenter.SetMusicEnabled(eventArg.IsOn);
        public void OnValueChanged(SliderEventArg<SliderHandlerMusicVolume> eventArg) => Presenter.SetMusicVolume(eventArg.Value);
        public void OnValueChanged(SwitchEventArg<SwitchHandlerSfxEnabled> eventArg) => Presenter.SetSfxEnabled(eventArg.IsOn);
        public void OnValueChanged(SliderEventArg<SliderHandlerSfxVolume> eventArg) => Presenter.SetSfxVolume(eventArg.Value);
        public void OnClick(ButtonEventArg<ButtonHandlerSave> eventArg) => Presenter.SaveSettings();
        public override void OnClick(ButtonEventArg<PopupButtonClose> eventArg) => Presenter.CancelSettings();
    }
}
