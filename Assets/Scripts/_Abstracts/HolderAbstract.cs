using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class HolderAbstract : MonoBehaviour, IHolder
{
    [SerializeField] protected Food food;
    [FormerlySerializedAs("plate")] [SerializeField] protected Cookware cookware;
    [SerializeField] protected Transform placeTransform;

  

    public Food GetFood() => food;
    public Cookware GetPlate() => cookware;
    public bool IsContainPlate() => cookware != null;
    public bool IsContainFood() => food != null;
    
    private void OnDrawGizmos()
    {
        if (placeTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeTransform.position, .2f);
    }

    public void ExchangeItems(HolderAbstract holder)
    {
        // 1. handle player have plate
        // 2. Put food in plate
        // 2.5 Swap plate if can put food in plate
        // 3. swap food
        var isHavePlate = IsContainPlate() || holder.IsContainPlate();
        if (isHavePlate)
        {
            if (PutFoodInPlate(holder) != false) return;
            SwapPlate(holder);
            Debug.Log("Swap cookware", holder.gameObject);
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
        Debug.Log("Put food in cookware", holder.gameObject);
        var plate = (this.cookware != null ? this.cookware : holder.GetPlate());
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
        SwapItems(holder, h => h.GetPlate(), (h, item) => h.SetPlate(item as Cookware));
    }

    private void SwapItems(HolderAbstract holder,
        Func<HolderAbstract, PickUpAbtract> getItem,
        Action<HolderAbstract, PickUpAbtract> setItem)
    {
        // generic method for swap cookware and food
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
    public void SetPlate(Cookware newCookware) => ResetItem<Cookware>(newCookware, ref this.cookware);

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
        // 1. Food in cookware
        // 2. Cookware
        // 2.5 Cannot have food and cookware in same time
        // 3. Food
        if (cookware != null)
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
        if (cookware.IsContainFoodInPlate())
        {
            cookware.DeleteAllFood();
        }
        else
        {
            cookware.Delete();
        }
    }

}