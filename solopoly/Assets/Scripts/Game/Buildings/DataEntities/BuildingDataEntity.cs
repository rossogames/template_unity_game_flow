using RossoGames.Common.Enums;
using RossoGames.Currencies.DataTypes;
using UnityEngine;

namespace RossoGames.Buildings.DataEntities
{
    [CreateAssetMenu(fileName = nameof(BuildingDataEntity), menuName = "RossoGames/Data Entities/Building")]
    public class BuildingDataEntity : ScriptableObject
    {
        public string Id => base.name;

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public Rarities Rarity { get; private set; }

        [field: SerializeField]
        public CurrencyAmount Cost { get; private set; }
    }
}
