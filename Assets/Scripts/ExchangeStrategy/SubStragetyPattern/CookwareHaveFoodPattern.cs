public partial class ExchangeCookwarePattern
{

    private class CookwareHaveFoodPattern : IExchangeStrategy
    {
        public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            var cookware1 = holder1.GetCookware();
            var cookware2 = holder2.GetCookware();
            var bothHaveFoodInCookware = cookware1.IsContainFoodInPlate() ||
                                          cookware2.IsContainFoodInPlate();
            if (!bothHaveFoodInCookware) return false;
            // var noPlateTypeCookware = cookware1.GetCookwareType() != CookwareType.Plate &&
            //                            cookware2.GetCookwareType() != CookwareType.Plate;
            // if(!noPlateTypeCookware) return false;
            var canExchangeFoodBetweenCookware = cookware1.CanPutFoodIn(cookware2.GetFood()) &&
                                                  cookware2.CanPutFoodIn(cookware1.GetFood());
            return canExchangeFoodBetweenCookware;
        }

        public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            // TODO: Need to check if can combine food
            var cookware1 = holder1.GetCookware();
            var cookware2 = holder2.GetCookware();
            var food1 = cookware1.GetFood();
            var food2 = cookware2.GetFood();
            cookware1.Swap(food2);
            cookware2.Swap(food1);
        }
    }
}
