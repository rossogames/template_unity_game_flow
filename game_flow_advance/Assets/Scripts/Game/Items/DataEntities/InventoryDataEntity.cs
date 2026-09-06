using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.Items.DataEntities
{
    [CreateAssetMenu(fileName = nameof(InventoryDataEntity), menuName = "Rossogames/Data Entities/Inventory")]
    public class InventoryDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public List<ItemDataEntity> Items { get; private set; }
    }
}
