using NaughtyAttributes;
using System;
using UnityEngine;

public abstract partial class HolderAbstract : MonoBehaviour, IHolder
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
   
  
        //Debug.Log("After Angle :" + angle );
        ExchangeManager.Exchange(this, player);

    }
    public Vector2 direction;

    public void SwapFoodAndCookwareOneWay(HolderAbstract holder)
    {
        var food1 = holder.GetFood();
        var food2 = GetFood();
        var cookware1 = holder.GetCookware();
        var cookware2 = GetCookware();
        if (cookware1 != null && cookware1.CanSwapFood(food2))
        {
            AddFoodToCookware(food2, cookware1);
            SetItem(null);
        }
        else if (cookware2 != null && cookware2.CanSwapFood(food1))
        {
            AddFoodToCookware(food1, cookware2);
            holder.SetItem(null);
        }

        void AddFoodToCookware(Food food, Cookware cookware)
        {
            cookware.Swap(food);
        }
    }

    public void SwapCookwareTwoWay(HolderAbstract holder)
    {
        var cookware1 = holder.GetCookware();
        var cookware2 = GetCookware();
        // TODO: Some container cannot put plate in ?, pls change it
        // Debug.LogWarning("TODO: Some container cannot put plate in ?, pls change it");
        if (CanContainCookware(cookware1) && holder.CanContainCookware(cookware2))
        {
            Debug.Log("Can container cookware");
            holder.SetItem(cookware2);
            SetItem(cookware1);
        }
        else
        {
            Debug.Log("Can not container cookware");

        }
    }

    public void SwapFoodTwoWay(HolderAbstract holder)
    {

        var food1 = holder.GetFood();
        var food2 = GetFood();
        if (CanHoldFood(food1) && holder.CanHoldFood(food2))
        {
            holder.SetItem(food2);
            SetItem(food1);
        }
    }

    public bool CanHoldFood(Food food)
    {
        if (CanHoldType(ContainerType.Food) == false) return false;
        return true;
    }
    public bool CanContainCookware(Cookware cookware)
    {
        if (CanHoldType(ContainerType.Cookware) == false) return false;
        return true;
    }

    private bool CanHoldType(ContainerType _Type)
    {
        return type == _Type || type == ContainerType.All;
    }
   
    public bool IsContainFoodInCookware()
    {
        return GetCookware() != null && GetCookware().IsContainFoodInPlate();
    }

    public void SetItem(PickUpAbtract newItem)
    {
        ResetItem(newItem);
    }

    private void ResetItem(PickUpAbtract newItem)
    {
        
        newItem?.SetToParentAndPosition(placeTransform);
        item = newItem;

        if(item != null)
        {
            item.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void DiscardFood()
    {
        if (IsContainFoodInCookware())
        {
            GetCookware().DiscardFood();
        }
        else
        {
            item.Discard();
            SetItem(null);
        }

    }
    [Button]
    public void DiscardCookware()
    {
        if (IsContainCookware())
        {
            item.Discard();
            SetItem(null);
        }
    }


    public CookwareType GetCookwareType()
    {
        var cookware = GetCookware();
        if (cookware == null) return CookwareType.None;
        return cookware.GetCookwareType();
    }

}