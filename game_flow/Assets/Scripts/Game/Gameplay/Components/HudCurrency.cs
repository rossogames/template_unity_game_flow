using Rossoforge.Events.Bus;
using Rossoforge.Events.Service;
using Rossoforge.Services.Locator;
using Rossogames.Currencies.DataEntities;
using Rossogames.Currencies.DataStructures;
using Rossogames.Currencies.Events;
using Rossogames.Currencies.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rossogames.Gameplay.Components
{
    public class HudCurrency : MonoBehaviour, IEventListener<CurrencyAmountChangedEvent>
    {
        [SerializeField]
        private CurrencyDataEntity _currencyDataEntity;

        [SerializeField]
        private Image _imageIcon;

        [SerializeField]
        private TextMeshProUGUI _labelAmount;

        private IEventService _eventService;
        private ICurrencyService _currencyService;

        private void Awake()
        {
            _eventService = ServiceLocator.Get<IEventService>();
            _currencyService = ServiceLocator.Get<ICurrencyService>();
        }

        private void Start()
        {
            SetAmountText();
            SetIcon();
        }

        private void OnEnable()
        {
            _eventService.RegisterListener<CurrencyAmountChangedEvent>(this);
        }

        private void OnDisable()
        {
            _eventService.UnregisterListener<CurrencyAmountChangedEvent>(this);
        }

        private void SetAmountText()
        {
            var currentAmount = _currencyService.GetCurrencyAmount(_currencyDataEntity);
            SetAmountText(currentAmount);
        }
        private void SetAmountText(CurrencyAmount currencyAmount)
        {
            if (currencyAmount.CurrencyDataEntity.Equals(_currencyDataEntity))
                _labelAmount.text = currencyAmount.Amount.ToString();
        }
        private void SetIcon()
        {
            _imageIcon.sprite = _currencyDataEntity.Icon;
        }

        public void OnEventInvoked(CurrencyAmountChangedEvent eventArg)
        {
            SetAmountText(eventArg.Amount);
        }
    }
}
