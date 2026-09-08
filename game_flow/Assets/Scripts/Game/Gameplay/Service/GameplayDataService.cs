using Rossogames.Gameplay.DataBehaviour;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Rossogames.Gameplay.Service
{
    [CreateAssetMenu(fileName = nameof(GameplayDataService), menuName = "Rossogames/Data Service/Gameplay")]
    public class GameplayDataService : ScriptableObject
    {
        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Stand By")]
#endif
        public PhaseStandByDataBehaviour StandByDataBehaviour { get; private set; }

        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Level Load")]
#endif
        public PhaseLevelLoadDataBehaviour LevelLoadDataBehaviour { get; private set; }

        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Level Unload")]
#endif
        public PhaseLevelUnloadDataBehaviour LevelUnloadDataBehaviour { get; private set; }

        [field: SerializeField]
#if ODIN_INSPECTOR
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Exploration")]
#endif
        public PhaseExplorationDataBehaviour ExplorationDataBehaviour { get; private set; }
    }
}