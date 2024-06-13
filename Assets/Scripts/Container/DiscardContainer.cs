using UnityEngine;

public class DiscardContainer : MonoBehaviour, IHolder
{
    public void ExchangeItems(HolderAbstract holder)
    {
        if (holder.IsContainFoodInCookware())
        {
            holder.GetCookware().DiscardFood();
        }
    }
}