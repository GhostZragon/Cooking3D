using EasyButtons;
using UnityEngine;

public class BaseContainer<T> : MonoBehaviour where T : PickUpAbtract
{
    public T prefab;
    public T RetrieveRawFood()
    {
        var pickUpAbtract = Instantiate(prefab);
        return pickUpAbtract;
    }

}