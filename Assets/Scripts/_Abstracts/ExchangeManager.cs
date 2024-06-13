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
        var cookware1 = holder1.GetCookware();
        var cookware2 = holder2.GetCookware();
        if(cookware1 == null || cookware2 == null)
        {
            holder1.SwapCookwareTwoWay(holder2);
            return;
        }
        bool bothHaveFoodInCookware = cookware1.IsContainFoodInPlate() &&
                                      cookware2.IsContainFoodInPlate();
        bool noPlateTypeCookware = cookware1.IsEqualCookwareType(CookwareType.Plate) == false &&
                                   cookware2.IsEqualCookwareType(CookwareType.Plate) == false;
        bool canExchangeFoodBetweenCookware = cookware1.CanPutFoodIn(cookware2.GetFood()) &&
                                              cookware2.CanPutFoodIn(cookware1.GetFood());
        // Scenario 1
        if (bothHaveFoodInCookware && noPlateTypeCookware && canExchangeFoodBetweenCookware)
        {
            Debug.Log("Swap food in both cookware");
        }
        else if (cookware1.IsContainFoodInPlate() || cookware2.IsContainFoodInPlate())
        {
            Debug.Log("some cookware have food and need exchange");
        }
        else if (cookware1.IsContainFoodInPlate() == false && cookware2.IsContainFoodInPlate() == false)
        {
            Debug.Log("no food in cookware, and just need swap it");
            holder1.SwapCookwareTwoWay(holder2);
        }
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
