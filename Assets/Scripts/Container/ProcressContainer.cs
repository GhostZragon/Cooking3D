using UnityEngine;

public class ProcressContainer : HolderAbstract
{
    [SerializeField] private FoodState foodStateWantToChange;

    public override void ExchangeItems(HolderAbstract player)
    {
        if (!CanProcess(player)) return;
        var foodData = FoodManager.instance.GetFood(player.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
    }

    private bool CanProcess(HolderAbstract player)
    {
        return player.GetCookware() == null && player.GetFood() != null;
    }
}