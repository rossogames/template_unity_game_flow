using Rossoforge.Services.Locator;
using Rossoforge.Services.Service;
using Rossogames.Inputs.Service;
using UnityEngine;

namespace Rossogames.Cameras.Service
{
    public class CameraService : ICameraService, IInitializable, IUpdatable, ILateUpdatable
    {
        private IInputsService _inputsService;

        private Camera _camera;
        private Transform _cameraRoot;
        private Transform _target;

        public float _currentRotationAngle;
        public float _targetRotationAngle;
        private float _rotationInput;
        private float _rotationSensitivity;

        private CameraDataService _dataService;

        public Camera Camera => _camera;
        public Vector3 CameraRootPosition => _cameraRoot.position;
        public Vector3 CenterPosition { get; private set; }
        public bool AllowMove { get; set; }
        public bool AllowRotate { get; set; }

        public CameraService(CameraDataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _inputsService = ServiceLocator.Get<IInputsService>();
        }

        public void Update()
        {
            UpdateRotationAngle();
        }
        public void LateUpdate()
        {
            if (_target == null)
                return;

            UpdateRotationData();
            Rotate();
            Move();
        }

        public void SetupCamera(Camera camera, Transform root, Transform target)
        {
            _camera = camera;
            _cameraRoot = root;
            _target = target;

            _targetRotationAngle = _dataService.InitRotationY;
            _cameraRoot.position = new Vector3(
                _cameraRoot.position.x,
                _dataService.Height,
                _cameraRoot.position.z
            );

            _camera.transform.rotation = Quaternion.Euler(new Vector3(
                    _dataService.InitRotationX,
                    _camera.transform.rotation.eulerAngles.y,
                    _camera.transform.rotation.eulerAngles.z
                ));
        }
        /*
        public void SetZoom(float value)
        {
            _camera.orthographicSize = Mathf.Clamp(value, _serviceData.RangeZoom.Min, _serviceData.RangeZoom.Max);
        }
        public void ZoomCamera(float value)
        {
            var cameraSize = _camera.orthographicSize -= value * _serviceData.SensitivityZoom;
            _camera.orthographicSize = Mathf.Clamp(cameraSize, _serviceData.RangeZoom.Min, _serviceData.RangeZoom.Max);
        }*/
        public void SetPosition(Vector3 position)
        {
            _cameraRoot.position = position;
        }

        private void UpdateRotationData()
        {
            if (_inputsService.Camera.IsMouseRotation)
            {
                _rotationSensitivity = _dataService.SensitivityRotationMouse;
                _rotationInput = _inputsService.Camera.RotationMouse;
                return;
            }

            _rotationSensitivity = _dataService.SensitivityRotationJoystick;
            _rotationInput = _inputsService.Camera.RotationJoystick;
        }
        private void UpdateRotationAngle()
        {
            if (Mathf.Abs(_rotationInput) > 0.01f)
                _targetRotationAngle += _rotationInput * _rotationSensitivity * Time.deltaTime;
        }
        private void Rotate()
        {
            if (!AllowRotate)
                return;

            _currentRotationAngle = Mathf.LerpAngle(_currentRotationAngle, _targetRotationAngle, Time.deltaTime * _rotationSensitivity);
            _cameraRoot.rotation = Quaternion.Euler(0, _currentRotationAngle, 0);
        }
        private void Move()
        {
            if (!AllowMove)
                return;

            var targetPosition = new Vector3(_target.position.x, _cameraRoot.transform.position.y, _target.position.z);
            _cameraRoot.transform.position = Vector3.Lerp(_cameraRoot.transform.position, targetPosition, Time.deltaTime * _dataService.SensitivityMove);
        }
    }
}