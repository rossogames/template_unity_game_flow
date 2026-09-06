using Rossogames.Currencies.DataEntities;
using System;
using UnityEngine;

namespace Rossogames.Currencies.DataStructures
{
    [Serializable]
    public struct CurrencyAmount
    {

        [field: SerializeField]
        public CurrencyDataEntity CurrencyDataEntity { get; private set; }

        [field: SerializeField]
        public int Amount { get; private set; }

        public CurrencyAmount(CurrencyDataEntity currencyData, int amount)
        {
            CurrencyDataEntity = currencyData;
            Amount = amount;
        }
    }
}
