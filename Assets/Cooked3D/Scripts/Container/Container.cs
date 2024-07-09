using NaughtyAttributes;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ContainerSelect))]
public class Container : HolderAbstract
{
    private void OnEnable()
    {
        if(placeTransform == null)
        {
            LoadPlaceTransform();
        }   
    }
    [Button]
    public virtual void LoadPlaceTransform()
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
}
