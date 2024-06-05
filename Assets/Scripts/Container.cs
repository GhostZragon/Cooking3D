using UnityEngine;
public interface IHolder
{
    IItem item { get; set; }
}
public interface IItem
{
    void SetParent(IHolder holder);
    void RemoveParent();
}
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
