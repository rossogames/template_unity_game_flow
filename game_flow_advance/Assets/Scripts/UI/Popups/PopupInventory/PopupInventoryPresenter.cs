using Rossoforge.Pool.Service;
using Rossoforge.Popups.UI;
using Rossoforge.Services.Locator;
using Rossogames.Common;
using UnityEngine;

namespace Rossogames.Popups.Inventory
{
    public class PopupInventoryPresenter : PopupPresenter<PopupInventoryView, PopupInventoryPresenter, PopupInventoryData>
    {
        private IPoolService _poolService;

        public PopupInventoryPresenter(PopupInventoryView view) : base(view)
        {
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        public override void OnOpening()
        {
            base.OnOpening();
            LoadItemsView();
        }

        private void LoadItemsView()
        {
            for (int i = 0; i < base.Data.InventoryData.Items.Count; i++)
            {
                var itemView = _poolService.Get<InventoryItemView>(View.ItemViewAssetReference, View.Container, Vector3.zero, Space.Self, PoolCategories.Gameplay);
                itemView.Initialize(base.Data.InventoryData.Items[i]);
            }
        }
    }
}
