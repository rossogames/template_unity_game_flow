using Rossoforge.Core.UserData;
using Rossoforge.Services;
using RossoGames.Progression.Data;

namespace RossoGames.Progression.Service
{
    public class ProgressionService : IProgressionService
    {
        private IUserDataService<SaveData> _userDataService;

        private ProgressionServiceData _serviceData;

        public ProgressionService(ProgressionServiceData serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
            _userDataService = ServiceLocator.Get<IUserDataService<SaveData>>();
            _userDataService.Load();
        }

        public void SaveProgression()
        {
            var currentSave = _userDataService.CurrentSave;
            currentSave.Version = 1;
            _userDataService.Save();
        }
    }
}
