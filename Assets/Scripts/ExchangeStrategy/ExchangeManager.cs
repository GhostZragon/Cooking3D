using System.Collections.Generic;
using UnityEngine;

public interface IExchangeStrategy
{
    bool CanExchange(HolderAbstract holder1, HolderAbstract holder2);
    void Exchange(HolderAbstract holder1, HolderAbstract holder2);
}
public static class ExchangeManager
{
    private static List<IExchangeStrategy> exchangeStrategies = new List<IExchangeStrategy>()
    {
        new FoodExchangeStrategy(),
        new CookwareExchangeStrategy(),
        new CookwareAndFoodExchangeStrategy()
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
}
