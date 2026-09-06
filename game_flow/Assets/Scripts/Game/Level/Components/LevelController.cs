using Rossoforge.Services.Locator;
using Rossogames.Level.DataContext;
using Rossogames.Level.Service;
using UnityEngine;

namespace Rossogames.Level.Components
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private LevelRootsDataContext _levelRoots;

        private ILevelService _levelService;

        private void Awake()
        {
            _levelService = ServiceLocator.Get<ILevelService>();
            _levelService.LoadLevel(_levelRoots);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _levelService?.OnDrawGizmos();
        }
#endif
    }
}
