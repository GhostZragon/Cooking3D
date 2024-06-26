using UnityEngine;

public partial class ExchangeCookwareAndFoodPattern
{
    /// <summary>
    /// Checking have any Food in cookware to swap
    /// </summary>
    private class ExchangeCookwareItemAndFoodPattern : IExchangeStrategy
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
            // 
            if (IsHaveCookwareNonFoodAndFood(holder1,holder2))
            {
                SwapFoodAndCookwareContents(holder1, holder2);
            }
            else if (IsHaveCookwareNonFoodAndFood(holder2, holder1))
            {
                SwapFoodAndCookwareContents(holder2, holder1);
            }
        }
        /// <summary>
        /// Checking Holder 1 is have cookware,
        ///          Holder 2 is have food
        /// </summary>
        /// <param name="holder1">"Holder" need check cookware</param>
        /// <param name="holder2">"Holder" need check food</param>
        /// <returns></returns>
        private bool IsHaveCookwareNonFoodAndFood(HolderAbstract holder1, HolderAbstract holder2)
        {
            return holder1.IsContainCookware() && holder2.IsContainFood();
        }
        private void SwapFoodAndCookwareContents(HolderAbstract holder1, HolderAbstract holder2)
        {
            // TODO: Combine food feature here
            Cookware targetCookware = holder1.GetCookware();
            Food targetFood = holder2.GetFood();
            Food foodInCookware = targetCookware.GetFood();
            Debug.LogWarning("TODO: Combine food feature here");

            if (FoodManager.instance.CanCombineFood(targetFood, foodInCookware, out FoodData foodData))
            {
                UITextPopupHandle.ShowTextAction(foodInCookware.transform.position, "Combine food success", Color.blue);
                
                // init new food
                if (foodData == null) return;
                targetCookware.CombineFood(targetFood);
                foodInCookware.SetData(foodData);
                foodInCookware.SetModel();
                
                holder2.DiscardFood();
            }
            else if (targetCookware.CanPutFoodIn(targetFood) && holder2.CanHoldFood(foodInCookware))
            {
                // swap food of COOKWARE and food of holder
                
                targetCookware.Swap(targetFood);

                holder2.SetItem(foodInCookware);
            }
        }
    }
}