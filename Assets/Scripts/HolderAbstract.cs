using UnityEngine;

public abstract class HolderAbstract : MonoBehaviour
{
    protected PickUpAbtract food;
    protected PickUpAbtract plate;
    [SerializeField] protected Transform placeTransform;
    public PickUpAbtract GetFood() => food;
    public PickUpAbtract GetPlate() => plate;
    public Transform GetPlaceTransform() => placeTransform;
    private void OnDrawGizmos()
    {
        if(placeTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(placeTransform.position, .2f);
        }
    }

    public void SwapFood(HolderAbstract holder)
    {
        var myFood = food;
        var holderFood = holder.GetFood();
        holder.SetFood(myFood);
        SetFood(holderFood);
    }
    public void SetFood(PickUpAbtract newFood) => ResetUltis(newFood, ref this.food);
    public void SetPlate(PickUpAbtract newPlate) =>  ResetUltis(newPlate, ref this.plate);
 
    private void ResetUltis(PickUpAbtract newItem, ref PickUpAbtract item)
    {
        if (newItem != null)
        {
            newItem.SetToParentAndPosition(placeTransform);
        }

        item = newItem;
    }
}