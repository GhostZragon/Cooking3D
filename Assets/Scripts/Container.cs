using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Container : BaseContainer<Transform>
{
    public Transform PlaceTransform;
    [Button]
    public override void SetItemInPlace(Transform item)
    {
        this.item = item;
        item.SetParent(PlaceTransform);
        item.transform.position = Vector3.zero;
        item.transform.position = PlaceTransform.position;
    }


    public override Transform TryGetItem()
    {
        if (item != null)
        {
            item.parent = null;
            return item;
        }
        return null;
    }
}

public abstract class BaseContainer<T> : MonoBehaviour
{
    public T item;

    public abstract void SetItemInPlace(T item);
    public abstract T TryGetItem();
}
