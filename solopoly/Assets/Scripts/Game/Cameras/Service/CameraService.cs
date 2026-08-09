using Rossoforge.Core.DataStructures;
using UnityEngine;

namespace RossoGames.Cameras.Service
{
    public class CameraService : ICameraService
    {
        private Camera _camera;
        private Transform _cameraRoot;
        private float _maxMovementRad;

        private CameraServiceData _serviceData;

        public Camera Camera => _camera;
        public Vector3 CameraRootPosition => _cameraRoot.position;
        public Vector3 CenterPosition { get; private set; }

        public CameraService(CameraServiceData serviceData)
        {
            _serviceData = serviceData;
        }
        public void Initialize()
        {
        }
        public void Dispose()
        {
        }

        public void SetupCamera(Camera camera, Transform root)
        {
            _camera = camera;
            _cameraRoot = root;
        }
        public void SetBounds(Size3<int> mapSize)
        {
            Vector3 _mapCenterPosition = new(
                ((float)mapSize.Width - 1) * 0.5f,
                1f,
                ((float)mapSize.Depth - 1) * 0.5f
            );

            CenterPosition = _mapCenterPosition;
            _cameraRoot.position = CenterPosition;
            _maxMovementRad = Mathf.Max(mapSize.Width, mapSize.Depth) * 0.5f;
        }
        public void SetZoom(float value)
        {
            _camera.orthographicSize = Mathf.Clamp(value, _serviceData.RangeZoom.Min, _serviceData.RangeZoom.Max);
        }
        public void ZoomCamera(float value)
        {
            var cameraSize = _camera.orthographicSize -= value * _serviceData.SensitivityZoom;
            _camera.orthographicSize = Mathf.Clamp(cameraSize, _serviceData.RangeZoom.Min, _serviceData.RangeZoom.Max);
        }
        public void MoveCamera(Vector3 displacement)
        {
            var displacementPosition = Quaternion.Euler(0, _cameraRoot.rotation.eulerAngles.y, 0) * displacement * _serviceData.SensitivityMove;
            _cameraRoot.Translate(-displacementPosition, Space.World);

            float distanceToCenter = (_cameraRoot.position - CenterPosition).magnitude;
            if (distanceToCenter > _maxMovementRad)
            {
                var direction = (_cameraRoot.position - CenterPosition).normalized;
                _cameraRoot.position = CenterPosition + direction * _maxMovementRad;
            }
        }
        public void RotateCamera(float yRotation, Vector3 axis)
        {
            float angle = yRotation * _serviceData.SensitivityRotation;
            _cameraRoot.RotateAround(axis, Vector3.up, angle);
        }
        public void SetPosition(Vector3 position)
        {
            _cameraRoot.position = position;
        }
    }
}