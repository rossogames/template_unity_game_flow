using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossogames.ContextActions.DataEntities;
using Rossogames.Inputs.Enums;
using Rossogames.Inputs.Events;
using Rossogames.Inputs.Service;
using Rossogames.Level.Service;
using Rossogames.LevelObjects.Components;
using TMPro;
using UnityEngine;

namespace Rossogames.ContextActions.Components
{
    public class ContextActionsMenuItem : MonoBehaviour,
        IEventListener<InputTypeChangedEvent>,
        IEventListener<ContextActionInputPressedEvent>
    {
        [SerializeField]
        private ContextActionInput _actionInput;

        [SerializeField]
        private TMP_Text _labelAction;

        [SerializeField]
        private GameObject _inputKeyboard;

        [SerializeField]
        private GameObject _inputJoystick;

        private ILevelService _levelService;
        protected IEventService _eventService;
        protected IInputsService _inputsService;

        private Interactable _interactable;
        private ContextActionDataEntity _dataEntity;

        private void Awake()
        {
            _levelService = ServiceLocator.Get<ILevelService>();
            _eventService = ServiceLocator.Get<IEventService>();
            _inputsService = ServiceLocator.Get<IInputsService>();
        }
        private void OnEnable()
        {
            _eventService.RegisterListener<InputTypeChangedEvent>(this);
            _eventService.RegisterListener<ContextActionInputPressedEvent>(this);

            SetInputsUI(_inputsService.CurrentInputType);
        }
        private void OnDisable()
        {
            _eventService.UnregisterListener<InputTypeChangedEvent>(this);
            _eventService.UnregisterListener<ContextActionInputPressedEvent>(this);
        }

        public void Initialize(Interactable interactable, ContextActionDataEntity contextActionDataEntity)
        {
            _interactable = interactable;
            _dataEntity = contextActionDataEntity;

            SetLabel();
        }

        public void OnButtonClick()
        {
            InvokeAction(_actionInput);
        }

        private void SetLabel()
        {
            _labelAction.text = _dataEntity.Name;
        }

        private void SetInputsUI(InputType inputType)
        {
            _inputKeyboard.SetActive(inputType == InputType.KeyboardMouse);
            _inputJoystick.SetActive(inputType == InputType.Controller);
        }
        private void InvokeAction(ContextActionInput actionInput)
        {
            if (_actionInput == actionInput)
                _levelService.InvokeContextAction(_dataEntity, _interactable);
        }

        public void OnEventInvoked(InputTypeChangedEvent eventArg)
        {
            SetInputsUI(eventArg.InputType);
        }
        public void OnEventInvoked(ContextActionInputPressedEvent eventArg)
        {
            InvokeAction(eventArg.ActionInput);
        }
    }
}
