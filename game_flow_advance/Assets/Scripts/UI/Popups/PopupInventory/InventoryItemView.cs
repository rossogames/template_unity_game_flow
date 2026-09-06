using Rossogames.Items.DataEntities;
using UnityEngine;
using UnityEngine.UI;

namespace Rossogames.Popups.Inventory
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField]
        private Image _imageIcon;

        private ItemDataEntity _itemDataEntity;

        public void Initialize(ItemDataEntity itemDataEntity)
        {
            _itemDataEntity = itemDataEntity;
            SetIcon();
        }

        private void SetIcon()
        {
            _imageIcon.sprite = _itemDataEntity.Icon;
        }
    }
}
