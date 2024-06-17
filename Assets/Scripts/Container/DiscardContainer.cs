using UnityEngine;

public class DiscardContainer : MonoBehaviour, IHolder
{
    public void ExchangeItems(HolderAbstract player)
    {
        if (player.IsContainFoodInCookware())
        {
            player.GetCookware().DiscardFood();
        }
    }
}