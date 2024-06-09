using System;
using NaughtyAttributes;
using UnityEngine;
public abstract class HolderAbstract : MonoBehaviour, IHolder
{
    [SerializeField] protected Food food;
    [SerializeField] protected Plate plate;
    [SerializeField] protected Transform placeTransform;

  

    public Food GetFood() => food;
    public Plate GetPlate() => plate;
    public bool IsContainPlate() => plate != null;
    public bool IsContainFood() => food != null;
    
    private void OnDrawGizmos()
    {
        if (placeTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeTransform.position, .2f);
    }

    public void ExchangeItems(HolderAbstract holder)
    {
        var isHavePlate = IsContainPlate() || holder.IsContainPlate();
        if (isHavePlate)
        {
            if (PutFoodInPlate(holder) != false) return;
            SwapPlate(holder);
            Debug.Log("Swap plate", holder.gameObject);
        }
        else
        {
            SwapFood(holder);
            Debug.Log("Swap food", holder.gameObject);
        }
    }

    private bool PutFoodInPlate(HolderAbstract holder)
    {
        // Just swap when have food in one of holder
        if (!IsContainFood() && !holder.IsContainFood()) return false;
        Debug.Log("Put food in plate", holder.gameObject);
        var plate = (this.plate != null ? this.plate : holder.GetPlate());
        var food = (this.food != null ? this.food : holder.GetFood());
        if (plate.CanPutFoodIn(food) == false) return false;

        plate.Add(food);
        holder.SetFood(null);
        SetFood(null);
        return true;
    }

    private void SwapFood(HolderAbstract holder)
    {
        SwapItems(holder, h => h.GetFood(), (h, item) => h.SetFood(item as Food));
    }

    private void SwapPlate(HolderAbstract holder)
    {
        SwapItems(holder, h => h.GetPlate(), (h, item) => h.SetPlate(item as Plate));
    }

    private void SwapItems(HolderAbstract holder,
        Func<HolderAbstract, PickUpAbtract> getItem,
        Action<HolderAbstract, PickUpAbtract> setItem)
    {
        // generic method for swap plate and food
        // 1. Get current item in holder 1
        // 2. Get item in holder 2
        // 3. Assign item of holder 1 to holder 2,
        // 3.1then repeat it with item of holder 2 to holder 1
        var myItem = getItem(this);
        var holderItem = getItem(holder);
        setItem(holder, myItem);
        setItem(this, holderItem);
    }

    public void SetFood(Food newFood) => ResetItem<Food>(newFood, ref this.food);
    public void SetPlate(Plate newPlate) => ResetItem<Plate>(newPlate, ref this.plate);

    private void ResetItem<T>(T newItem, ref T item) where T : PickUpAbtract
    {
        // 1. check if new item is valid, set it to new position
        // 2. assign references in holder
        // 2.1 if item new is null, then this item of that holder is null        
        newItem?.SetToParentAndPosition(placeTransform);
        item = newItem;
    }

    public void DiscardInHandItem()
    {
        // priority in discard (use case)
        // 1. Food in plate
        // 2. Plate
        // 2.5 Cannot have food and plate in same time
        // 3. Food
        if (plate != null)
        {
            DiscardPlate();
        }
        else
        {
            food?.Delete();
        }
    }

    private void DiscardPlate()
    {
        if (plate.IsContainFoodInPlate())
        {
            plate.DeleteAllFood();
        }
        else
        {
            plate.Delete();
        }
    }

}