using Rossoforge.Popups.UI;
using Rossogames.Items.DataEntities;

namespace Rossogames.Popups.Inventory
{
    public class PopupInventoryData : IPopupData
    {
        public InventoryDataEntity InventoryData { get; private set; }

        public PopupInventoryData(InventoryDataEntity inventoryData)
        {
            InventoryData = inventoryData;
        }
    }
}
