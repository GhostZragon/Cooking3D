using UnityEngine;

public class ProcressContainer : HolderAbstract
{
    [SerializeField] private FoodState foodStateWantToChange;

 
    public override void ExchangeItems(HolderAbstract player)
    {
        if (!CanProcess(player)) return;
        var foodData = FoodManager.instance.GetFoodData(player.GetFoodType(),foodStateWantToChange);
        if (foodData == null) return;
        var food = player.GetFood();
        food.SetData(foodData);
        food.SetModel();
        player.SwapFoodTwoWay(this);
    }

    private bool CanProcess(HolderAbstract player)
    {
        return !player.IsContainCookware() && player.IsContainFood();
    }
}

public class ExchangeStrategyTest : FoodExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return false;
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
    }
}