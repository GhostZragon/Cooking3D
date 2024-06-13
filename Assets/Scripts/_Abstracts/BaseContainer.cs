using UnityEngine;

public abstract class BaseContainer<T> : MonoBehaviour, IHolder where T : PickUpAbtract
{
    public T prefab;

    protected T RetrieveRawFood()
    {
        var pickUpAbtract = Instantiate(prefab);
        return pickUpAbtract;
    }

    protected static bool CanStopContinueSwap(HolderAbstract holder)
    {
        Debug.Log("On checking");
        if (holder.GetFood() != null || holder.GetCookware() != null) return false;
        return true;
    }
    public abstract void ExchangeItems(HolderAbstract holder);
}