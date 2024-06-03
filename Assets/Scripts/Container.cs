using UnityEngine;

public enum ContainerType
{
    Resource,
    Container
}
public class Container : MonoBehaviour
{
    public Transform PlaceTransform;
    public Transform Item;
    [SerializeField] private ContainerType type;
    public ContainerType GetType() => type;
    private void OnDrawGizmos()
    {
        if(PlaceTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(PlaceTransform.position, .2f);
        }
    }


}
