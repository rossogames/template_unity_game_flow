using Rossoforge.Core.DataStructures;
using Rossoforge.Core.Services;
using UnityEngine;

namespace RossoGames.Cameras.Service
{
    public interface ICameraService : IService
    {
        Camera Camera { get; }
        Vector3 CameraRootPosition { get; }
        Vector3 CenterPosition { get; }

        void SetupCamera(Camera camera, Transform root);
        void SetBounds(Size3<int> mapSize);
        void SetZoom(float value);
        void ZoomCamera(float value);
        void MoveCamera(Vector3 displacement);
        void RotateCamera(float yRotation, Vector3 axis);
        void SetPosition(Vector3 position);
    }
}