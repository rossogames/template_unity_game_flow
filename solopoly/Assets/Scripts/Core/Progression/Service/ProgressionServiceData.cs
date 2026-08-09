using RossoGames.Buildings.DataEntities;
using UnityEngine;

namespace RossoGames.Progression.Service
{
    [CreateAssetMenu(fileName = nameof(ProgressionServiceData), menuName = "RossoGames/Service Data/Progression")]
    public class ProgressionServiceData : ScriptableObject
    {
        [field: SerializeField]
        public BuildingDataEntity[] StarterBuildings { get; private set; }
    }
}
