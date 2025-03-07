﻿using System.Collections.Generic;
using UnityEngine;

public interface IExchangeStrategy
{
    bool CanExchange(HolderAbstract holder1, HolderAbstract holder2);
    void Exchange(HolderAbstract holder1, HolderAbstract holder2);
}

internal static class ExchangeManager
{
    private static List<IExchangeStrategy> exchangeStrategies = new List<IExchangeStrategy>()
    {
        new ExchangeFoodPattern(),
        new ExchangeCookwarePattern(),
        new ExchangeCookwareAndFoodPattern()
    };

    public static void Exchange(HolderAbstract currentHolder, HolderAbstract holderWantToChange)
    {
        foreach (var strategy in exchangeStrategies)
        {
            if (strategy.CanExchange(currentHolder, holderWantToChange))
            {
                Debug.Log("Type Exchange is " + strategy.ToString());
                strategy.Exchange(currentHolder, holderWantToChange);
                break;
            }
        }
    }
    public static void Exchange(HolderAbstract currentHolder, HolderAbstract holderWantToChange,IExchangeStrategy strategy)
    {
        if (strategy.CanExchange(currentHolder, holderWantToChange))
        {
            Debug.Log("Type Exchange is " + strategy.ToString());
            strategy.Exchange(currentHolder, holderWantToChange);
        }
    }
}
