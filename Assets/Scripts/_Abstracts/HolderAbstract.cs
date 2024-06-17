using UnityEngine;
using UnityEngine.Serialization;

public abstract class HolderAbstract : MonoBehaviour, IHolder
{
    [SerializeField] protected Food food;

    [FormerlySerializedAs("plate")]
    [SerializeField]
    protected Cookware cookware;

    [SerializeField] protected Transform placeTransform;
    [SerializeField] protected ContainerType type;

    public Food GetFood() => food;
    public Cookware GetCookware() => cookware;

    private void OnDrawGizmos()
    {
        if (placeTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeTransform.position, .2f);
    }
    public virtual void ExchangeItems(HolderAbstract player)
    {
        Debug.Log("On Swap");
        ExchangeManager.Exchange(this, player);
    }
    private void AddFoodToCookware(Food food, Cookware cookware)
    {
        cookware.Add(food);
    }
    public void SwapFoodAndCookwareOneWay(HolderAbstract holder)
    {
        var food1 = holder.GetFood();
        var food2 = food;
        var cookware1 = holder.GetCookware();
        var cookware2 = cookware;
        if (cookware1 != null && cookware1.CanPutFoodIn(food2))
        {
            AddFoodToCookware(food2, cookware1);
            SetFood(null);
        }
        else if (cookware2 != null && cookware2.CanPutFoodIn(food1))
        {
            AddFoodToCookware(food1, cookware2);
            holder.SetFood(null);
        }
    }
    public void SwapCookwareTwoWay(HolderAbstract holder)
    {
        // TODO: Some container cannot put plate in ?, pls change it
        Debug.LogWarning("TODO: Some container cannot put plate in ?, pls change it");
        if (CanPutCookwareIn() && holder.CanPutCookwareIn())
        {
            var cookware1 = holder.GetCookware();
            var cookware2 = cookware;

            holder.SetPlate(cookware2);
            SetPlate(cookware1);
        }

    }
    public void SwapFoodTwoWay(HolderAbstract holder)
    {
        if (CanPutFoodIn() && holder.CanPutFoodIn())
        {
            var food1 = holder.GetFood();
            var food2 = food;

            holder.SetFood(food2);
            SetFood(food1);
        }
    }

    public bool CanPutFoodIn() => type == ContainerType.Food || type == ContainerType.All;
    public bool CanPutCookwareIn() => type == ContainerType.Cookware || type == ContainerType.All;
    public void SetFood(Food newFood) => ResetItem<Food>(newFood, ref this.food);
    public void SetPlate(Cookware newCookware) => ResetItem<Cookware>(newCookware, ref this.cookware);
    public bool IsContainFoodInCookware()
    {
        return cookware != null && cookware.IsContainFoodInPlate();
    }

    private void ResetItem<T>(T newItem, ref T item) where T : PickUpAbtract
    {
        // 1. check if new item is valid, set it to new position
        // 2. assign references in currentHolder
        // 2.1 if item new is null, then this item of that currentHolder is null        
        newItem?.SetToParentAndPosition(placeTransform);
        item = newItem;
    }

    public void DiscardFood()
    {
        if (IsContainFoodInCookware())
        {
            cookware.DiscardFood();
        }else if (food != null)
        {
            Destroy(food.gameObject);
            SetFood(null);
        }
    }
}