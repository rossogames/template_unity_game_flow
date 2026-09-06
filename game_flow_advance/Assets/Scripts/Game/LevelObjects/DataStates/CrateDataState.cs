using Rossogames.Progression.DataState;
using System;

namespace Rossogames.LevelObjects.DataStates
{
    [Serializable]
    public class CrateDataState : BaseDataState
    {
        public bool IsOpened { get; set; }
    }
}