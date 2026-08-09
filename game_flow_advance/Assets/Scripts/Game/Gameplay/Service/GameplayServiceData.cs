using RossoGames.Gameplay.DataBehaviour;
using RossoGames.Gameplay.DataEntities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Gameplay.Service
{
    [CreateAssetMenu(fileName = nameof(GameplayServiceData), menuName = "RossoGames/Service Data/Gameplay")]
    public class GameplayServiceData : ScriptableObject
    {
        [field: SerializeField]
        [field: BoxGroup("Data Entities")]
        [field: LabelText("Run Setup")]
        public RunSetupDataEntity RunSetupDataEntity { get; private set; }

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
    }
}