using Rossoforge.Services.Service;
using Rossogames.Items.DataEntities;
using Rossogames.Popups.Pause;
using Rossogames.Popups.Question;
using UnityEngine;

namespace Rossogames.PopupFlow.Service
{
    public interface IPopupFlowService : IService
    {
        Awaitable<QuestionResult> OpenConfirmQuit();
        void OpenSettings();
        Awaitable<PopupPauseData> OpenPause();
        Awaitable OpenPopupContainer(ContainerDataEntity containerDataEntity);
        Awaitable OpenPopupInventory(InventoryDataEntity inventoryDataEntity);
    }
}
