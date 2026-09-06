using Rossoforge.Audio.Service;
using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossoforge.Utils.IO;
using UnityEngine;

namespace Rossogames.Settings.Service
{
    public class SettingsService : ISettingsService, IInitializable
    {
        private const string _settingKeyMusicVolume = "MusicVolume";
        private const string _settingKeySfxVolume = "SfxVolume";
        private const string _settingKeyMusicEnabled = "MusicEnabled";
        private const string _settingKeySfxEnabled = "SfxEnabled";
        private const string _settingKeyVSyncCount = "VSyncCount";
        private const string _settingKeyTargetFrameRate = "TargetFrameRate";

        private IAudioService _audioService;

        private SettingsDataService _dataService;
        private float _musicVolume;
        private float _sfxVolume;
        private bool _sfxEnabled;
        private bool _musicEnabled;
        private bool _vSyncCount;
        private int _targetFrameRate;

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                _audioService.SetChannelVolume(_dataService.MusicChannelData, _musicVolume);
            }
        }
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                _audioService.SetChannelVolume(_dataService.SfxChannelData, _sfxVolume);
            }
        }
        public bool MusicEnabled
        {
            get => _musicEnabled;
            set
            {
                _musicEnabled = value;
                _audioService.SetChannelMute(_dataService.MusicChannelData, !_musicEnabled);
            }
        }
        public bool SfxEnabled
        {
            get => _sfxEnabled;
            set
            {
                _sfxEnabled = value;
                _audioService.SetChannelMute(_dataService.SfxChannelData, !_sfxEnabled);
            }
        }
        public bool VSync
        {
            get => _vSyncCount;
            set
            {
                _vSyncCount = value;
                QualitySettings.vSyncCount = value ? 1 : 0;
            }
        }
        public int TargetFrameRate
        {
            get => _targetFrameRate;
            set
            {
                _targetFrameRate = value;
                Application.targetFrameRate = value;
            }
        }

        public SettingsService(SettingsDataService dataService)
        {
            _dataService = dataService;
        }

        public void Initialize()
        {
            _audioService = ServiceLocator.Get<IAudioService>();

            Load();
        }

        public void Load()
        {
            MusicEnabled = PlayerPrefsStorage.LoadBool(_settingKeyMusicEnabled, true);
            MusicVolume = PlayerPrefsStorage.LoadFloat(_settingKeyMusicVolume, .8f);

            SfxEnabled = PlayerPrefsStorage.LoadBool(_settingKeySfxEnabled, true);
            SfxVolume = PlayerPrefsStorage.LoadFloat(_settingKeySfxVolume, 1f);

            VSync = PlayerPrefsStorage.LoadBool(_settingKeyVSyncCount, true);
            TargetFrameRate = PlayerPrefsStorage.LoadInt(_settingKeyTargetFrameRate, 120);
        }

        public void Save()
        {
            PlayerPrefsStorage.SaveBool(_settingKeyMusicEnabled, MusicEnabled);
            PlayerPrefsStorage.SaveFloat(_settingKeyMusicVolume, MusicVolume);

            PlayerPrefsStorage.SaveBool(_settingKeySfxEnabled, SfxEnabled);
            PlayerPrefsStorage.SaveFloat(_settingKeySfxVolume, SfxVolume);

            PlayerPrefsStorage.SaveBool(_settingKeyVSyncCount, VSync);
            PlayerPrefsStorage.SaveInt(_settingKeyTargetFrameRate, TargetFrameRate);
        }
    }
}
