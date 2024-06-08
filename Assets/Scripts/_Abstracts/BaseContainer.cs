using EasyButtons;
using UnityEngine;

public abstract class BaseContainer<T> : MonoBehaviour, IHolder where T : PickUpAbtract
{
    public T prefab;
    public T RetrieveRawFood()
    {
        var pickUpAbtract = Instantiate(prefab);
        return pickUpAbtract;
    }

    public abstract void ExchangeItems(HolderAbstract holder);
}