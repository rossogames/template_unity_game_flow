using RossoGames.LevelObjects.DataBehaviour;
using UnityEngine;

namespace RossoGames.LevelObjects.Components
{
    public class Highlightable : MonoBehaviour
    {
        [field: SerializeField] private Outline[] Outlines { get; set; }

        public void ShowHighlight(OutlineDataBehaviour outlineDataBehaviour)
        {
            if (outlineDataBehaviour == null)
                return;

            foreach (Outline outline in Outlines)
            {
                outline.ShowOutline(outlineDataBehaviour);
                outline.enabled = true;
            }
        }

        public void HideHighlight()
        {
            foreach (Outline outline in Outlines)
            {
                if (this == null || outline == null)
                    return;

                outline.enabled = false;
            }
        }
    }
}
