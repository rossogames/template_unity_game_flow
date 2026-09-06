using Rossoforge.Services.Locator;
using Rossogames.Common;
using Rossogames.Level.Service;
using Rossogames.LevelObjects.DataBehaviour;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField]
        public InteractableDataBehaviour DataBehaviour { get; private set; }

        private ILevelService _levelService;
        private IAutomaticInteractionEnter _automaticInteractionEnter;
        private IAutomaticInteractionExit _automaticInteractionExit;
        private Highlightable _highlightable;

        public bool InZone { get; private set; }

        private void Awake()
        {
            _levelService = ServiceLocator.Get<ILevelService>();

            TryGetComponent<Highlightable>(out _highlightable);
            TryGetComponent<IAutomaticInteractionEnter>(out _automaticInteractionEnter);
            TryGetComponent<IAutomaticInteractionExit>(out _automaticInteractionExit);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;

            InZone = true;
            if (_highlightable != null)
                _highlightable.Show();

            if (_automaticInteractionEnter != null)
            {
                _automaticInteractionEnter.OnAutomaticInteractionEnter();
                return;
            }

            _levelService.TryShowContextActions(this);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;

            InZone = false;
            if (_highlightable != null)
                _highlightable.Hide();

            _levelService.HideContextActions();
            if (_automaticInteractionExit != null)
                _automaticInteractionExit.OnAutomaticInteractionExit();
        }
    }
}
