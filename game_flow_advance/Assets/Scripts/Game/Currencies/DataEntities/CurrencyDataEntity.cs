using UnityEngine;

namespace RossoGames.Currencies.DataEntities
{
    [CreateAssetMenu(fileName = nameof(CurrencyDataEntity), menuName = "RossoGames/Data Entities/Currency")]
    public class CurrencyDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }
}
