using Rossoforge.Services;
using RossoGames.Currencies.DataEntities;
using RossoGames.Currencies.DataTypes;
using RossoGames.Currencies.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayCheats : MonoBehaviour
{
    private ICurrencyService _currencyService;

    [SerializeField]
    private CurrencyDataEntity _currencyDataEntity;

    [SerializeField]
    private int _currencyAmount = 10;

    private void Awake()
    {
        _currencyService = ServiceLocator.Get<ICurrencyService>();
    }

    private void Update()
    {
        AddCurrency();
    }

    private void AddCurrency()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            var amount = new CurrencyAmount(_currencyDataEntity, _currencyAmount);
            _currencyService.AddCurrency(amount);
        }
    }
}
