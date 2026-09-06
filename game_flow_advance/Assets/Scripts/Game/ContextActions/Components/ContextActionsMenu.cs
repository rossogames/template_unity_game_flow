using Rossoforge.Services.Locator;
using Rossogames.Inputs.Enums;
using Rossogames.Level.Service;
using Rossogames.LevelObjects.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Rossogames.ContextActions.Components
{
    public class ContextActionsMenu : MonoBehaviour
    {
        [SerializeField]
        private ContextActionsMenuItem[] _actionItems;

        private ILevelService _levelService;
        private Interactable _interactable;

        private void Awake()
        {
            _levelService = ServiceLocator.Get<ILevelService>();


        }
        public void Initialize(Interactable interactable)
        {
            _interactable = interactable;
            LoadActionOptions();
        }

        private void LoadActionOptions()
        {
            var actionItemsDict = new Dictionary<ContextActionInput, ContextActionsMenuItem>()
            {
                { ContextActionInput.Input1, _actionItems[0] },
                { ContextActionInput.Input2, _actionItems[1] },
                { ContextActionInput.Input3, _actionItems[2] }
            };

            foreach (var interactableAction in _interactable.DataBehaviour.InteractableContextActions)
            {
                if (!actionItemsDict.TryGetValue(interactableAction.Input, out var itemMenu))
                    continue;

                if (!_levelService.IsContextActionAvailable(interactableAction.ContextAction, _interactable))
                    continue;

                actionItemsDict.Remove(interactableAction.Input);
                itemMenu.Initialize(_interactable, interactableAction.ContextAction);
                itemMenu.gameObject.SetActive(true);
            }

            foreach (var itemMenu in actionItemsDict.Values)
                itemMenu.gameObject.SetActive(false);
        }
    }
}
