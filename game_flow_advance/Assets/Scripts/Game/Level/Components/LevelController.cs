using RossoGames.Level.DataTypes;
using RossoGames.Level.Service;
using UnityEngine;

namespace RossoGames.Level.Components
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private LevelRoots _levelRoots;

        private ILevelService _levelService;

        private void Awake()
        {
            _levelService = ServiceLocator.Get<ILevelService>();
            _levelService.LoadLevel(_levelRoots);
        }
    }
}
