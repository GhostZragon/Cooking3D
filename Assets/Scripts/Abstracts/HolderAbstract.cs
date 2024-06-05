using UnityEngine;

public abstract class HolderAbstract : MonoBehaviour
{
    [SerializeField] protected PickUpAbtract food;
    [SerializeField] protected PickUpAbtract plate;
    [SerializeField] protected Transform placeTransform;
    public PickUpAbtract GetFood() => food;
    public PickUpAbtract GetPlate() => plate;
    public Transform GetPlaceTransform() => placeTransform;

    private void OnDrawGizmos()
    {
        if (placeTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(placeTransform.position, .2f);
        }
    }

    protected void ExchangeItems(HolderAbstract holder)
    {
        if (plate != null || holder.GetPlate() != null)
        {
            if (PutFoodInPlate(holder) == false)
            {
                SwapPlate(holder);
            }
        }
        else
        {
            SwapFood(holder);
        }
    }
    private bool PutFoodInPlate(HolderAbstract holder)
    {
        if (food != null || holder.GetFood() != null)
        {
            var plate = (this.plate != null ? this.plate : holder.GetPlate()).GetComponent<Plate>();
            var food = (this.food != null ? this.food : holder.GetFood()).GetComponent<Food>();
            plate.Add(food);
            SetFood(null);
            return true;
        }
        return false;
    }
    private void SwapFood(HolderAbstract holder)
    {
        var myFood = food;
        var holderFood = holder.GetFood();
        holder.SetFood(myFood);
        SetFood(holderFood);
    }

    private void SwapPlate(HolderAbstract holder)
    {
        var myPlate = plate;
        var holderPlate = holder.GetPlate();
        holder.SetPlate(myPlate);
        SetPlate(holderPlate);
    }

    public void SetFood(PickUpAbtract newFood) => ResetUltis(newFood, ref this.food);
    public void SetPlate(PickUpAbtract newPlate) => ResetUltis(newPlate, ref this.plate);

    private void ResetUltis(PickUpAbtract newItem, ref PickUpAbtract item)
    {
        if (newItem != null)
        {
            newItem.SetToParentAndPosition(placeTransform);
        }

        item = newItem;
    }
}