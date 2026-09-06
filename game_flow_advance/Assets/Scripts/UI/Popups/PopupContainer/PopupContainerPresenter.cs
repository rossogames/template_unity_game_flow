using Rossoforge.Pool.Service;
using Rossoforge.Popups.UI;
using Rossoforge.Services.Locator;
using Rossogames.Common;
using UnityEngine;

namespace Rossogames.Popups.Container
{
    public class PopupContainerPresenter : PopupPresenter<PopupContainerView, PopupContainerPresenter, PopupContainerData>
    {
        private IPoolService _poolService;

        public PopupContainerPresenter(PopupContainerView view) : base(view)
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
            for (int i = 0; i < base.Data.ContainerData.Items.Count; i++)
            {
                var itemView = _poolService.Get<ItemView>(View.ItemViewAssetReference, View.Container, Vector3.zero, Space.Self, PoolCategories.Gameplay);
                itemView.Initialize(base.Data.ContainerData.Items[i]);
            }
        }
    }
}
