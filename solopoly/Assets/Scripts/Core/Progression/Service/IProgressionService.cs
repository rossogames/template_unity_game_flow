using Rossoforge.Core.Services;

namespace RossoGames.Progression.Service
{
    public interface IProgressionService : IService, IInitializable
    {
        string[] GetUnlockedBuildings();
        void SaveProgression();
    }
}
