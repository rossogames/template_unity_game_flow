using Rossoforge.Services.Service;

namespace Rossogames.Settings.Service
{
    public interface ISettingsService : IService
    {
        float MusicVolume { get; set; }
        float SfxVolume { get; set; }
        bool MusicEnabled { get; set; }
        bool SfxEnabled { get; set; }
        bool VSync { get; set; }
        int TargetFrameRate { get; set; }

        void Load();
        void Save();
    }
}
