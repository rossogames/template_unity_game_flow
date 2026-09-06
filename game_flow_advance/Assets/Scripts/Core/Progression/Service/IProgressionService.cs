using Rossoforge.Services.Service;

namespace RossoGames.Progression.Service
{
    public interface IProgressionService : IService, IInitializable
    {
        void SaveProgression();
    }
}
