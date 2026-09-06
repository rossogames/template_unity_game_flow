using Rossoforge.Pool.DataConfig;
using Rossoforge.Popups.UI;
using UnityEngine;

namespace Rossogames.Popups.Inventory
{
    public class PopupInventoryView : PopupView<PopupInventoryView, PopupInventoryPresenter, PopupInventoryData>
    {
        [field: SerializeField]
        public Transform Container { get; set; }

        [field: SerializeField]
        public PooledGameobjectDataConfig ItemViewAssetReference { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            base.Presenter = new PopupInventoryPresenter(this);
        }
    }
}
