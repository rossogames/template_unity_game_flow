using Rossoforge.Core.DataStructures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RossoGames.Gameplay.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(PhaseDiceRollDataBehaviour), menuName = "RossoGames/Data Behaviour/Gameplay Phases/Dice Roll")]
    public class PhaseDiceRollDataBehaviour : GameplayBasePhaseDataBehaviour
    {
        [field: SerializeField]
        [field: BoxGroup("Dice")]
        [field: LabelText("Value Range")]
        public RangeNumber<int> DiceValueRange { get; private set; }
    }
}
