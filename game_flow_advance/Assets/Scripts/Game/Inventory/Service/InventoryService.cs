using Rossoforge.Services.Service;

namespace Rossogames.Inventory.Service
{
    public class InventoryService : IInventoryService, IInitializable
    {
        private InventoryDataService _dataService;

        public InventoryService(InventoryDataService dataService)
        {
            _dataService = dataService;
        }

        public void Initialize()
        {
        }
    }
}
