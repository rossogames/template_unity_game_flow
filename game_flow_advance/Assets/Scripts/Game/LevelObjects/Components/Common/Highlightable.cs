using Rossogames.LevelObjects.DataBehaviour;
using UnityEngine;

namespace Rossogames.LevelObjects.Components
{
    [RequireComponent(typeof(Outline))]
    public class Highlightable : MonoBehaviour
    {
        [SerializeField]
        private OutlineDataBehaviour _dataBehaviour;

        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.SetOutline(_dataBehaviour);

        }
        public void Show()
        {
            _outline.enabled = true;
        }

        public void Hide()
        {
            _outline.enabled = false;
        }
    }
}
