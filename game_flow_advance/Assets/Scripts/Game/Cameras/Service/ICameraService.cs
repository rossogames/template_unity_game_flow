using Rossoforge.Services.Service;
using UnityEngine;

namespace Rossogames.Cameras.Service
{
    public interface ICameraService : IService
    {
        Camera Camera { get; }
        Vector3 CameraRootPosition { get; }
        Vector3 CenterPosition { get; }
        bool AllowMove { get; set; }
        bool AllowRotate { get; set; }

        void SetupCamera(Camera camera, Transform root, Transform target);
    }
}