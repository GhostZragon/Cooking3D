using UnityEngine;

public class CustomerContainer : HolderAbstract
{
    public override void ExchangeItems(HolderAbstract player)
    {
        if (!CanDeliverFood(player)) return;
        base.ExchangeItems(player);
    }

    private bool CanDeliverFood(HolderAbstract player)
    {
        if (player.IsContainFoodInCookware() == false) return false;
        if (player.GetCookwareType() != CookwareType.Plate) return false;
        var food = player.GetCookware().GetFood();
        Debug.Log("Food name: "+food.name);
        player.DiscardCookware();
        return true;
    }
}