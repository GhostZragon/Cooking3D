using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Container : MonoBehaviour
{
    public Transform item;
    public Transform PlaceTransform;
    [Button]
    public void SetItemInPlace(Transform Item)
    {
        item = Item;
        item.SetParent(PlaceTransform);
        item.transform.position = Vector3.zero;
        item.transform.position = PlaceTransform.position;
    }
    public Transform TryGetItem()
    {
        if(item != null)
        {
            item.parent = null;
            return item;
        }
        return null;
    }
}
