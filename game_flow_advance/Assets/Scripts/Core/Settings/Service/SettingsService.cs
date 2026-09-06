using Rossoforge.Utils.IO;

namespace RossoGames.Settings.Service
{
    public class SettingsService : ISettingsService
    {
        private const string _settingKeyMusicVolume = "MusicVolume";
        private const string _settingKeySfxVolume = "SfxVolume";
        private const string _settingKeyMusicEnabled = "MusicEnabled";
        private const string _settingKeySfxEnabled = "SfxEnabled";

        private IAudioService _audioService;

        private SettingsDataService _serviceData;
        private float _musicVolume;
        private float _sfxVolume;
        private bool _sfxEnabled;
        private bool _musicEnabled;


        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                _audioService.SetChannelVolume(_serviceData.MusicChannelData, _musicVolume);
            }
        }

        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                _audioService.SetChannelVolume(_serviceData.SfxChannelData, _sfxVolume);
            }
        }
        public bool MusicEnabled
        {
            get => _musicEnabled;
            set
            {
                _musicEnabled = value;
                _audioService.SetChannelMute(_serviceData.MusicChannelData, !_musicEnabled);
            }
        }

        public bool SfxEnabled
        {
            get => _sfxEnabled;
            set
            {
                _sfxEnabled = value;
                _audioService.SetChannelMute(_serviceData.SfxChannelData, !_sfxEnabled);
            }
        }

        public SettingsService(SettingsDataService serviceData)
        {
            _serviceData = serviceData;
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
        }

        public void Save()
        {
            PlayerPrefsStorage.SaveBool(_settingKeyMusicEnabled, MusicEnabled);
            PlayerPrefsStorage.SaveFloat(_settingKeyMusicVolume, MusicVolume);

            PlayerPrefsStorage.SaveBool(_settingKeySfxEnabled, SfxEnabled);
            PlayerPrefsStorage.SaveFloat(_settingKeySfxVolume, SfxVolume);
        }
    }
}
