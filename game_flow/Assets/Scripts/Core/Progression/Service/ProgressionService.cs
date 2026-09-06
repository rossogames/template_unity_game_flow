using Rossoforge.Persistence.Service;
using Rossogames.Progression.DataState;
using System.Collections.Generic;

namespace Rossogames.Progression.Service
{
    public class ProgressionService : PersistenceService<ProgressionData>, IProgressionService
    {
        private ProgressionDataService _dataService;
        public Dictionary<string, BaseDataState> DataStateCollection;

        public ProgressionService(ProgressionDataService dataService) : base(dataService)
        {
            _dataService = dataService;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SaveProgression()
        {
            Data.Version = 1;
            Save();
        }

        public T RegisterDataState<T>(string id) where T : BaseDataState, new()
        {
            if (!Data.DataStateCollection.TryGetValue(id, out var dataState))
            {
                dataState = new T();
                Data.DataStateCollection.Add(id, dataState);
            }

            return dataState as T;
        }
    }
}
