public class FoodExchangeStrategy : IExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.GetFood() != null || holder2.GetFood() != null) &&
            (holder1.GetCookware() == null && holder2.GetCookware() == null);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        holder1.SwapFoodTwoWay(holder2);
    }
}
