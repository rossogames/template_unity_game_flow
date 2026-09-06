using Rossoforge.Persistence.Service;
using Rossogames.Progression.DataState;
using System;
using System.Collections.Generic;

namespace Rossogames.Progression.Service
{
    [Serializable]
    public class ProgressionData : IPersistentData
    {
        public int Version { get; set; }
        public Dictionary<string, BaseDataState> DataStateCollection { get; set; }

        public ProgressionData()
        {
            DataStateCollection = new Dictionary<string, BaseDataState>();
        }
    }
}
