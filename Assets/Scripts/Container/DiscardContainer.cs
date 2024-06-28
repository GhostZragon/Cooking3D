using UnityEngine;

public class DiscardContainer : MonoBehaviour, IHolder
{
    public void ExchangeItems(HolderAbstract player)
    {
        player.DiscardFood();
    }
}