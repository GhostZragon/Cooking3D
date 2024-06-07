using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceContainer : MonoBehaviour
{
    public Food prefab;
    public Food RetrieveRawFood()
    {
        var food = Instantiate(prefab);
        return food;
    }
}

public interface IHolder
{
    void ExchangeItems(HolderAbstract holder);
}