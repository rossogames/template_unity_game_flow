using UnityEngine;

namespace Rossogames.Gameplay.DataBehaviour
{
    public abstract class GameplayBasePhaseDataBehaviour : ScriptableObject
    {
        [field: SerializeField]
        public bool AllowPause { get; private set; } = true;
    }
}
