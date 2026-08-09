using Rossoforge.Core.Events;
using Rossoforge.Services;
using Rossoforge.UI.Controls.Buttons;
using RossoGames.Buildings.DataEntities;
using RossoGames.Cards.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RossoGames.Cards.Components
{
    [RequireComponent(typeof(Button))]
    public class BuildingCard : MonoBehaviour, IButtonClickListener<BuildingCardButtonHandler>
    {
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TMP_Text _labelName;
        [SerializeField] private TMP_Text _labelRarity;
        [SerializeField] private TMP_Text _labelCost;

        private IEventService _eventService;

        private Button _button;

        public BuildingDataEntity DataEntity { get; private set; }

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _button = GetComponent<Button>();
        }

        public void Initialize(BuildingDataEntity buildingDataEntity)
        {
            DataEntity = buildingDataEntity;
            _imageIcon.sprite = DataEntity.Icon;
            _labelName.text = DataEntity.Name;
            _labelRarity.text = DataEntity.Rarity.ToString();
            _labelCost.text = DataEntity.Cost.Amount.ToString();
        }

        public void OnClick(ButtonEventArg<BuildingCardButtonHandler> eventArg)
        {
            _eventService.Raise(new BuildingCardClickedEvent(DataEntity));
        }
    }
}
