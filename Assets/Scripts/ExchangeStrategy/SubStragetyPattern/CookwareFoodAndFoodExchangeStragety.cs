using UnityEngine;

public partial class CookwareAndFoodExchangeStrategy
{
    /// <summary>
    /// Checking have any Food in cookware to swap
    /// </summary>
    private class CookwareFoodAndFoodExchangeStragety : IExchangeStrategy
    {
        public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            if (holder1.IsContainFoodInCookware() || 
                holder2.IsContainFoodInCookware())
                return true;
            return false;
        }

        public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            Debug.Log("holder contain targetFood");
            if (holder1.IsContainCookware() && holder2.IsContainFood())
            {
                SwapFoodAndCookwareContents(holder1, holder2);
            }
            else if (holder1.IsContainFood() && holder2.IsContainCookware())
            {
                SwapFoodAndCookwareContents(holder2, holder1);
            }
        }
        private void SwapFoodAndCookwareContents(HolderAbstract holder1, HolderAbstract holder2)
        {
            // TODO: Combine food feature here
            Cookware targetCookware = holder1.GetCookware();
            Food targetFood = holder2.GetFood();
            Food foodInCookware = targetCookware.GetFood();
            if (targetCookware.CanPutFoodIn(targetFood) && holder2.CanPutFoodIn())
            {
                targetCookware.Add(targetFood);
                holder2.SetItem(foodInCookware);
            }
        }
    }


}
