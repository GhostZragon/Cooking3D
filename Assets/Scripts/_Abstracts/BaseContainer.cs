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
        if (holder.IsContainFood() || holder.IsContainCookware()) return true;
        return false;
    }
    public abstract void ExchangeItems(HolderAbstract holder);
}