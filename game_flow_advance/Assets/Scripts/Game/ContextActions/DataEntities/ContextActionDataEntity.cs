using UnityEngine;

namespace Rossogames.ContextActions.DataEntities
{
    public abstract class ContextActionDataEntity : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
    }
}
