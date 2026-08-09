using UnityEngine;

namespace RossoGames.Level.Service
{
    public class LevelHandlerEnvironment : LevelHandlerBase
    {
        private readonly Transform _root;

        public LevelHandlerEnvironment(LevelServiceData serviceData, Transform root) : base(serviceData)
        {
            _root = root;
        }

        public override void Initialize()
        {
            Instantiate();
        }

        private void Instantiate()
        {
            /*
            var obj = GameObject.Instantiate(_serviceData.CurrentLevelDataAsset.EnvironmentAssetReference, _root);
            obj.transform.rotation = Quaternion.identity;

            var w = (float)_serviceData.CurrentLevelDataAsset.Size.Width;
            var h = (float)_serviceData.CurrentLevelDataAsset.Size.Height;
            obj.transform.position = new Vector3((w + 2) / 2, 0, (h + 2) / 2);
            */
        }
    }
}
