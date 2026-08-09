using UnityEngine;

namespace RossoGames.LevelObjects.DataAssets
{
    public abstract class LevelObjectDataAsset : ScriptableObject
    {
        [field: SerializeField]
        public GameObject AssetReference { get; private set; }
    }
}
