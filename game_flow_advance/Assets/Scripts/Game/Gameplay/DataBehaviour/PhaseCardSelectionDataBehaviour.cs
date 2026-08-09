using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Gameplay.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(PhaseCardSelectionDataBehaviour), menuName = "RossoGames/Data Behaviour/Gameplay Phases/Card Selection")]
    public class PhaseCardSelectionDataBehaviour : GameplayBasePhaseDataBehaviour
    {
        [field: SerializeField]
        [field: BoxGroup("Cards")]
        public int CardsAmount { get; private set; }
    }
}
