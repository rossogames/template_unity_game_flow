using Rossoforge.Core.Services;

namespace RossoGames.Progression.Service
{
    public interface IProgressionService : IService, IInitializable
    {
        void SaveProgression();
    }
}
