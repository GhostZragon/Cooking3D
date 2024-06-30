
using UnityEngine;

public partial class ExchangeCookwarePattern
{

    private class CookwareHaveFoodPattern : IExchangeStrategy
    {
        public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            var cookware1 = holder1.GetCookware();
            var cookware2 = holder2.GetCookware();
            //if (cookware1.IsContainFoodInPlate() == false|| cookware2.IsContainFoodInPlate() == false) return false;
            var cookware1IsPlate = cookware1.GetCookwareType() == CookwareType.Plate;
            var cookware2IsPlate = cookware2.GetCookwareType() == CookwareType.Plate;
            var canExchangeFoodBetweenCookware = cookware1.CanSwapFood(cookware2.GetFood(), cookware2IsPlate) &&
                                                  cookware2.CanSwapFood(cookware1.GetFood(), cookware1IsPlate);
            return canExchangeFoodBetweenCookware;
        }

        public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            // TODO: Need to check if can combine food
            var cookware1 = holder1.GetCookware();
            var cookware2 = holder2.GetCookware();
            var plateCount = 0;

            if (cookware1.GetCookwareType() == CookwareType.Plate)
                plateCount++;
            if (cookware2.GetCookwareType() == CookwareType.Plate)
                plateCount++;

            if (plateCount == 2)
            {
                if (cookware1.CanCombineWithCookware(cookware2))
                {
                    cookware2.DiscardFood();
                    Debug.Log("Can swap 2 0");
                    return;
                }
            }
            else if (plateCount == 1)
            {
                var cookwareIsPlate = cookware1.GetCookwareType() == CookwareType.Plate ? cookware1 : cookware2;
                var cookwareAnother = cookware1.GetCookwareType() != CookwareType.Plate ? cookware2 : cookware1;
                if (cookwareIsPlate.CanCombineWithCookware(cookwareAnother))
                {
                    cookwareAnother.DiscardFood();
                    Debug.Log("Can swap 1 ");
                    return;
                }
            }
            else
            {
                SwapFood(cookware1, cookware2);

            }

            Debug.Log("This is same type of cookware");

        }
        private void SwapFood(Cookware cookware1,Cookware cookware2)
        {
            var food1 = cookware1.GetFood();
            var food2 = cookware2.GetFood();
            cookware1.Swap(food2);
            cookware2.Swap(food1);
        }
    }
}
