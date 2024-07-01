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
    private Transform playerTransform;
    public virtual void ExchangeItems(HolderAbstract player)
    {
        // Debug.Log("On Swap");

        var direction = GetNormalizedDirection(player.transform.position,transform.position);
        angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        //Debug.Log("Before Angle :" + angle);
        if (direction != Vector2.up && direction != Vector2.down)
        {
            angle -= 180;
        }
        //Debug.Log("Direction: " + direction);
        //Debug.Log("After Angle :" + angle );
        ExchangeManager.Exchange(this, player);
    }
    public float angle;
    private static Vector2 GetNormalizedDirection(Vector3 playerPosition, Vector3 holderPosition)
    {
        var newPlayerPos = new Vector2(playerPosition.x, playerPosition.z);
        var newHolderPos = new Vector2(holderPosition.x, holderPosition.z);
        Vector2 direction = newPlayerPos - newHolderPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            return Vector2.right;
        }
        else if (angle >= 45 && angle < 135)
        {
            return Vector2.up;
        }
        else if (angle >= 135 || angle < -135)
        {
            return Vector2.left;
        }
        else if (angle >= -135 && angle < -45)
        {
            return Vector2.down;
        }

        return Vector3.zero;
    }
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
            item.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void DiscardFood()
    {
        if (IsContainFoodInCookware())
        {
            GetCookware().DiscardFood();
        }
        else if (IsContainFood())
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