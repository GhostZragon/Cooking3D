using UnityEngine;

public class CustomerContainer : Container
{
    private void SpawnText(string text,Color color)
    {
        UITextPopupHandle.ShowTextAction?.Invoke(placeTransform.position, text, color);
    }
    public override void ExchangeItems(HolderAbstract player)
    {
        if (!CanDeliverFood(player))
        {
            SpawnText("I don't want this food!",Color.red);
            return;
        }
        base.ExchangeItems(player);
        SpawnText("Thank you!",Color.green);
        Debug.Log("On Swap");
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