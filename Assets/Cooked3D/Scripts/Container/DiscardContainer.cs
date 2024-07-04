using UnityEngine;

public class DiscardContainer : MonoBehaviour, IHolder
{
    public void ExchangeItems(HolderAbstract player)
    {
        if (player.IsContainFood())
        {
            player.DiscardFood();

        }
        else if (player.IsContainCookware())
        {
            player.DiscardCookware();

        }
    }
}