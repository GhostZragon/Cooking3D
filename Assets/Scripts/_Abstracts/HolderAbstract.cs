using NaughtyAttributes;
using System;
using UnityEngine;

public abstract class HolderAbstract : MonoBehaviour, IHolder
{
    [SerializeField] protected PickUpAbtract item;
    [SerializeField] protected Transform placeTransform;
    [SerializeField] protected ContainerType type;

    public Food GetFood() => item as Food;
    public Cookware GetCookware() => item as Cookware;
    public bool IsContainFood() => item is Food;
    public bool IsContainCookware() => item is Cookware;

    private void OnDrawGizmos()
    {
        if (placeTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeTransform.position, .2f);
    }

    public virtual void ExchangeItems(HolderAbstract player)
    {
        // Debug.Log("On Swap");
        ExchangeManager.Exchange(this, player);
    }

    public void SwapFoodAndCookwareOneWay(HolderAbstract holder)
    {
        var food1 = holder.GetFood();
        var food2 = GetFood();
        var cookware1 = holder.GetCookware();
        var cookware2 = GetCookware();
        if (cookware1 != null && cookware1.CanPutFoodIn(food2))
        {
            AddFoodToCookware(food2, cookware1);
            SetItem(null);
        }
        else if (cookware2 != null && cookware2.CanPutFoodIn(food1))
        {
            AddFoodToCookware(food1, cookware2);
            holder.SetItem(null);
        }
        
        void AddFoodToCookware(Food food, Cookware cookware)
        {
            cookware.Add(food);
        }
    }

    public void SwapCookwareTwoWay(HolderAbstract holder)
    {
        // TODO: Some container cannot put plate in ?, pls change it
        // Debug.LogWarning("TODO: Some container cannot put plate in ?, pls change it");
        if (CanPutCookwareIn() && holder.CanPutCookwareIn())
        {
            var cookware1 = holder.GetCookware();
            var cookware2 = GetCookware();

            holder.SetItem(cookware2);
            SetItem(cookware1);
        }
    }

    public void SwapFoodTwoWay(HolderAbstract holder)
    {
        if (CanPutFoodIn() && holder.CanPutFoodIn())
        {
            var food1 = holder.GetFood();
            var food2 = GetFood();

            holder.SetItem(food2);
            SetItem(food1);
        }
    }

    public bool CanPutFoodIn() => type == ContainerType.Food || type == ContainerType.All;
    public bool CanPutCookwareIn() => type == ContainerType.Cookware || type == ContainerType.All;
    // public void SetFood(Food newFood) => ResetItem<Food>(newFood, ref this.food);
    // public void SetPlate(Cookware newCookware) => ResetItem<Cookware>(newCookware, ref this.cookware);

    public bool IsContainFoodInCookware()
    {
        return GetCookware() != null && GetCookware() .IsContainFoodInPlate();
    }
    
    public void SetItem(PickUpAbtract newItem)
    {
        ResetItem(newItem);
    }
    
    private void ResetItem(PickUpAbtract newItem)
    {
        newItem?.SetToParentAndPosition(placeTransform);
        item = newItem;
    }

    public void DiscardFood()
    {
        if (IsContainFoodInCookware())
        {
            GetCookware().DiscardFood();
        }
        else if (IsContainFood())
        {
            Destroy(item.gameObject);
            SetItem(null);
        }
    }
    [Button]
    public void DiscardCookware()
    {
        if (GetCookware() != null)
        {
            Destroy(item.gameObject);
            SetItem(null);
        }
    }

    public FoodType GetFoodType()
    {
        var food = GetFood();
        if (food == null) return FoodType.None;
        return food.GetFoodType();
    }
    
    public CookwareType GetCookwareType()
    {
        var cookware = GetCookware();
        if (cookware == null) return CookwareType.None;
        return cookware.GetCookwareType();
    }

    public ContainerType GetContainerType()
    {
        return type;
    }
}