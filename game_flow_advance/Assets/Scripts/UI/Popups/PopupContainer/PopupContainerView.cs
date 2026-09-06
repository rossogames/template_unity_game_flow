using Rossoforge.Pool.DataConfig;
using Rossoforge.Popups.UI;
using UnityEngine;

namespace Rossogames.Popups.Container
{
    public class PopupContainerView : PopupView<PopupContainerView, PopupContainerPresenter, PopupContainerData>
    {
        [field: SerializeField]
        public Transform Container { get; set; }

        [field: SerializeField]
        public PooledGameobjectDataConfig ItemViewAssetReference { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            base.Presenter = new PopupContainerPresenter(this);
        }
    }
}
