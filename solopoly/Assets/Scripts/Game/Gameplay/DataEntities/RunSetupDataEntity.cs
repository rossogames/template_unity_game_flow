using RossoGames.Currencies.DataTypes;
using UnityEngine;

namespace RossoGames.Gameplay.DataEntities
{
    [CreateAssetMenu(fileName = nameof(RunSetupDataEntity), menuName = "RossoGames/Data Entities/Run Setup")]
    public class RunSetupDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public CurrencyAmount CurrencyAmount { get; private set; }
    }
}
