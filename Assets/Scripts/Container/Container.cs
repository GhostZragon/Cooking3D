using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Container : HolderAbstract
{
#if UNITY_EDITOR
    
    [CanEditMultipleObjects][Button]protected virtual void LoadPlaceTransform()
    {
        if (placeTransform == null)
        {
            var newPlaceTransform = new GameObject();
            newPlaceTransform.transform.parent = transform;
            newPlaceTransform.name = "PlaceTransform";
            newPlaceTransform.transform.localPosition = new Vector3(0, .5f, 0);
            placeTransform = newPlaceTransform.transform;
        }

        transform.tag = "Container";
        transform.gameObject.layer = 9;
    }
#endif
}

public class ProcessorContainer : HolderAbstract
{
    public FoodState FoodStateToConvert;
    public bool canSwap = false;
    private Food currentFood;
    public override void ExchangeItems(HolderAbstract holder)
    {
        
    }
}