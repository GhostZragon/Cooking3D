using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ContainerSelect))]
public class Container : HolderAbstract
{
#if UNITY_EDITOR
    private void OnEnable()
    {
        if(placeTransform == null)
        {
            LoadPlaceTransform();
        }   
    }
    [CanEditMultipleObjects][Button]
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
#endif
}
