using Rossoforge.Services.Service;

namespace RossoGames.Settings.Service
{
    public interface ISettingsService : IService, IInitializable
    {
        float MusicVolume { get; set; }
        float SfxVolume { get; set; }
        bool MusicEnabled { get; set; }
        bool SfxEnabled { get; set; }

        void Load();
        void Save();
    }
}
