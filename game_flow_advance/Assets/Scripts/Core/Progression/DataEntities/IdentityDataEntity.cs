using Sirenix.OdinInspector;
using UnityEngine;

namespace Rossogames.Progression.DataEntities
{
    [CreateAssetMenu(fileName = nameof(IdentityDataEntity), menuName = "Rossogames/Data Entities/Identity")]
    public class IdentityDataEntity : ScriptableObject
    {
        [field: SerializeField, ReadOnly]
        public string Id { get; private set; }

#if UNITY_EDITOR
        [Button("Generate GUID")]
        public void GenerateGuid()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = System.Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
