using UnityEngine;

namespace Rossogames.Currencies.DataEntities
{
    [CreateAssetMenu(fileName = nameof(CurrencyDataEntity), menuName = "Rossogames/Data Entities/Currency")]
    public class CurrencyDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }
}
