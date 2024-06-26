using UnityEngine;

public class CustomerContainer : Container
{
    [SerializeField] private bool allowNotContainFood = false;
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

        if (player.IsContainFoodInCookware() == false && allowNotContainFood == false) return false;
        if (player.GetCookwareType() != CookwareType.Plate && allowNotContainFood == false) return false;
        //var food = player.GetCookware().GetFood();
        player.DiscardCookware();
        return true;
    }
}