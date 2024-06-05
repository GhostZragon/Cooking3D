using UnityEngine;

public enum ContainerType
{
    Container,
    Resource
}
public class Container : MonoBehaviour
{
    public Transform PlaceTransform;
    public Food Item;
    public Plate plate;

    public bool IsContainPlate() => plate != null;
    private void OnDrawGizmos()
    {
        if(PlaceTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(PlaceTransform.position, .2f);
        }
    }


}
