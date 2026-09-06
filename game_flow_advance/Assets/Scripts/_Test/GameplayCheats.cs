using Rossoforge.Services.Locator;
using Rossogames.Currencies.DataEntities;
using Rossogames.Currencies.DataStructures;
using Rossogames.Currencies.Service;
using Rossogames.Items.DataEntities;
using Rossogames.PopupFlow.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayCheats : MonoBehaviour
{
    [SerializeField]
    public InventoryDataEntity _inventoryDataEntity;

    private ICurrencyService _currencyService;
    private IPopupFlowService _popupFlowService;

    [SerializeField]
    private CurrencyDataEntity _currencyDataEntity;

    [SerializeField]
    private int _currencyAmount = 10;

    private void Awake()
    {
        _currencyService = ServiceLocator.Get<ICurrencyService>();
        _popupFlowService = ServiceLocator.Get<IPopupFlowService>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        AddCurrency();
        OpenPopup();
    }
#endif

    private void AddCurrency()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            var amount = new CurrencyAmount(_currencyDataEntity, _currencyAmount);
            _currencyService.AddCurrency(amount);
        }
    }
    private void OpenPopup()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
            _popupFlowService.OpenPopupInventory(_inventoryDataEntity);
    }
}
