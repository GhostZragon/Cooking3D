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
        if (cookware1 == null || cookware2 == null)
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
            Cookware.Ultis.SwapFood(cookware1, cookware2);
        }
        else if (cookware1.IsContainFoodInPlate() || cookware2.IsContainFoodInPlate())
        {
            Debug.Log("some cookware have food and need exchange");
            Cookware.Ultis.SwapFood(cookware1, cookware2);
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
    private bool GetHolderHaveCookwareContainFood(HolderAbstract holder1, HolderAbstract holder2)
    {
        if (holder1.IsContainFoodInCookware())
            return true;
        if (holder2.IsContainFoodInCookware())
            return true;
        return false;
    }
    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        var holderContainFood = GetHolderHaveCookwareContainFood(holder1, holder2);

        if (holderContainFood)
        {
            
            Debug.Log("holder contain food");
            if (holder1.GetCookware() != null && holder2.GetFood() != null)
            {
                NewMethod(holder1, holder2);
            }
            else if (holder1.GetFood() != null && holder2.GetCookware() != null)
            {
                NewMethod(holder2, holder1);
            }
        }
        else
        {
            // need to swap food here
            Debug.Log("put food in cookware");
            holder1.SwapFoodAndCookware(holder2);
        }
    }

    private static void NewMethod(HolderAbstract holder1, HolderAbstract holder2)
    {
        Cookware cookware = null;
        Food food = null;
        cookware = holder1.GetCookware();
        var foodInCookware = cookware.GetFood();
        food = holder2.GetFood();
        if (cookware.CanPutFoodIn(food) && holder2.CanPutFoodIn())
        {
            cookware.Add(food);
            holder2.SetFood(foodInCookware);
        }
    }
}
