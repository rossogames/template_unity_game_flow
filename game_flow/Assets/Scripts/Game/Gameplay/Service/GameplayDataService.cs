using Rossogames.Gameplay.DataBehaviour;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Gameplay.Service
{
    [CreateAssetMenu(fileName = nameof(GameplayDataService), menuName = "Rossogames/Data Service/Gameplay")]
    public class GameplayDataService : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Stand By")]
        public PhaseStandByDataBehaviour StandByDataBehaviour { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Level Load")]
        public PhaseLevelLoadDataBehaviour LevelLoadDataBehaviour { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Level Unload")]
        public PhaseLevelUnloadDataBehaviour LevelUnloadDataBehaviour { get; private set; }

        [field: SerializeField]
        [field: BoxGroup("Phase Data Behaviour")]
        [field: LabelText("Exploration")]
        public PhaseExplorationDataBehaviour ExplorationDataBehaviour { get; private set; }
    }
}