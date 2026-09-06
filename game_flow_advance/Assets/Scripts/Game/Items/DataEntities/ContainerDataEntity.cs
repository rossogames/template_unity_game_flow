using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.Items.DataEntities
{
    [CreateAssetMenu(fileName = nameof(ContainerDataEntity), menuName = "Rossogames/Data Entities/Container")]
    public class ContainerDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public List<ItemDataEntity> Items { get; private set; }
    }
}
