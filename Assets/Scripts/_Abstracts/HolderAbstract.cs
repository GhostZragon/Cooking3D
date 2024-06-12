using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class HolderAbstract : MonoBehaviour, IHolder
{
    [SerializeField] protected Food food;

    [FormerlySerializedAs("plate")] [SerializeField]
    protected Cookware cookware;

    [SerializeField] protected Transform placeTransform;
    [SerializeField] protected ContainerType type;
    

    public Food GetFood() => food;
    public Cookware GetPlate() => cookware;

    private void OnDrawGizmos()
    {
        if (placeTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeTransform.position, .2f);
    }

    public virtual void ExchangeItems(HolderAbstract holder)
    {
        // int cookwareCount = 0;
        // int foodCount = 0;
        // var food1 = holder.GetFood();
        // var food2 = food;
        // var cookware1 = holder.GetPlate();
        // var cookware2 = cookware;
        //
        // if (food1 != null) foodCount++;
        // if (food2 != null) foodCount++;
        // if (cookware1 != null) cookwareCount++;
        // if (cookware2 != null) cookwareCount++;

        // holder.SetFood(food2);
        // SetFood(food1);
        SwapFoodTwoWay(holder);
    }

    private void SwapFoodTwoWay(HolderAbstract holder)
    {
        bool holder1CanPutFood = CanPutFoodIn();
        bool holder2CanPutFood = holder.CanPutFoodIn();
        if (holder1CanPutFood && holder2CanPutFood)
        {
            var food1 = holder.GetFood();
            var food2 = food;
            
            holder.SetFood(food2);
            SetFood(food1);
        }
    }

    public bool CanPutFoodIn() => type == ContainerType.Food || type == ContainerType.All;
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
}