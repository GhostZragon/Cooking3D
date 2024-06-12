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
                Debug.Log("Can Exchange");
                strategy.Exchange(currentHolder, holderWantToChange);
                break;
            }
        }
    }
}

public class FoodExchangeStrategy : IExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.GetFood() != null || holder2.GetFood() != null) &&
            (holder1.GetCookware() == null && holder2.GetCookware() == null);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        holder1.SwapFoodTwoWay(holder2);
    }
}

public class CookwareExchangeStrategy : IExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.GetCookware() != null || holder2.GetCookware() != null) &&
            (holder1.GetFood() == null && holder2.GetFood() == null);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        holder1.SwapCookwareTwoWay(holder2);
    }
}
public class CookwareAndFoodExchangeStrategy : IExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.GetCookware() != null || holder2.GetCookware() != null) &&
            (holder1.GetFood() == null || holder2.GetFood() == null);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        holder1.SwapFoodAndCookware(holder2);
    }
}
