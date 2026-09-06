using Rossogames.ContextActions.DataEntities;
using Rossogames.Inputs.Enums;
using System;
using UnityEngine;

namespace Rossogames.LevelObjects.DataBehaviour
{
    [CreateAssetMenu(fileName = nameof(InteractableDataBehaviour), menuName = "Rossogames/Data Behaviour/Level Objects/Interactable")]
    public class InteractableDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        public InteractableContextAction[] InteractableContextActions { get; private set; }
    }

    [Serializable]
    public class InteractableContextAction
    {
        [field: SerializeField]
        public ContextActionDataEntity ContextAction { get; private set; }

        [field: SerializeField]
        public ContextActionInput Input { get; set; }
    }
}
