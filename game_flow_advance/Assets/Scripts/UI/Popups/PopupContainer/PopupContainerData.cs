using Rossoforge.Popups.UI;
using Rossogames.Items.DataEntities;

namespace Rossogames.Popups.Container
{
    public class PopupContainerData : IPopupData
    {
        public ContainerDataEntity ContainerData { get; private set; }

        public PopupContainerData(ContainerDataEntity containerData)
        {
            ContainerData = containerData;
        }
    }
}
