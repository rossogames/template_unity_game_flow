using UnityEngine;

namespace Rossogames.Items.DataEntities
{
    [CreateAssetMenu(fileName = nameof(ItemDataEntity), menuName = "Rossogames/Data Entities/Item")]
    public class ItemDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }
    }
}
