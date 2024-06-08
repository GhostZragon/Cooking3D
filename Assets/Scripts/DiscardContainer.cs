using UnityEngine;

public class DiscardContainer : MonoBehaviour, IHolder
{
    public void ExchangeItems(HolderAbstract holder)
    {
        holder.DiscardInHandItem();
    }
}