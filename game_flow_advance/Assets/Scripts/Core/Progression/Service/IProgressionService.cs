using Rossoforge.Persistence.Service;
using Rossogames.Progression.DataState;

namespace Rossogames.Progression.Service
{
    public interface IProgressionService : IPersistenceService<ProgressionData>
    {
        void SaveProgression();
        T RegisterDataState<T>(string id) where T : BaseDataState, new();
    }
}
