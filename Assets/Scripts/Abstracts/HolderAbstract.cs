using System;
using UnityEngine;

public abstract class HolderAbstract : MonoBehaviour , IHolder
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

    public void ExchangeItems(HolderAbstract holder)
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
            var plate = (this.plate != null ? this.plate : holder.GetFood()).GetComponent<Plate>();
            var food = (this.food != null ? this.food : holder.GetFood()).GetComponent<Food>();
            plate.Add(food);
            holder.SetFood(null);
            SetFood(null);
            return true;
        }
        return false;
    }
  
    private void SwapFood(HolderAbstract holder)
    {
        SwapItems(holder, h => h.GetFood(), (h, item) => h.SetFood(item));
    }

    private void SwapPlate(HolderAbstract holder)
    {
        SwapItems(holder, h => h.GetPlate(), (h, item) => h.SetPlate(item));
    }
    
    private void SwapItems(HolderAbstract holder, 
        Func<HolderAbstract, PickUpAbtract> getItem, 
        Action<HolderAbstract, PickUpAbtract> setItem)
    {
        var myItem = getItem(this);
        var holderItem = getItem(holder);
        setItem(holder, myItem);
        setItem(this, holderItem);
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