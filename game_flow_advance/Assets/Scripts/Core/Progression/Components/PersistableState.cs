using Rossoforge.Services.Locator;
using Rossogames.Progression.DataEntities;
using Rossogames.Progression.DataState;
using Rossogames.Progression.Service;
using UnityEngine;

namespace Rossogames.Progression.Components
{
    public abstract class PersistableState<T> : MonoBehaviour where T : BaseDataState, new()
    {
        private IProgressionService _progressionService;

        [field: SerializeField]
        public IdentityDataEntity Identity { get; private set; }

        public T DataState { get; private set; }

        protected virtual void Awake()
        {
            _progressionService = ServiceLocator.Get<IProgressionService>();
            DataState = _progressionService.RegisterDataState<T>(Identity.Id);
        }
        protected virtual void OnEnable()
        {
            OnDataStateLoaded();
        }

        protected virtual void OnDataStateLoaded()
        {
        }
    }
}
