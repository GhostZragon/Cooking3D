public partial class CookwareExchangeStrategy
{

    private class CookwareFoodTransferStrategy : IExchangeStrategy
    {
        public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            var cookware1 = holder1.GetCookware();
            var cookware2 = holder2.GetCookware();
            bool bothHaveFoodInCookware = cookware1.IsContainFoodInPlate() ||
                                 cookware2.IsContainFoodInPlate();
            bool noPlateTypeCookware = cookware1.GetCookwareType() != CookwareType.Plate &&
                                       cookware2.GetCookwareType() != CookwareType.Plate;
            bool canExchangeFoodBetweenCookware = cookware1.CanPutFoodIn(cookware2.GetFood()) &&
                                                  cookware2.CanPutFoodIn(cookware1.GetFood());
            return bothHaveFoodInCookware && noPlateTypeCookware && canExchangeFoodBetweenCookware;
        }

        public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
        {
            // TODO: Need to check if can combine food
            Cookware.Ultis.SwapFoodTwoWay(holder1.GetCookware(), holder2.GetCookware());
        }
    }
}
