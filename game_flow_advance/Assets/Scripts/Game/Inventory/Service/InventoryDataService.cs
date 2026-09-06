using Rossogames.Items.DataEntities;
using UnityEngine;

namespace Rossogames.Inventory.Service
{
    [CreateAssetMenu(fileName = nameof(InventoryDataService), menuName = "Rossogames/Data Service/Inventory")]
    public class InventoryDataService : ScriptableObject
    {
        [SerializeField]
        private ContainerDataEntity _containerDataEntity;
    }
}
