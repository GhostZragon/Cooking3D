public class FoodExchangeStrategy : IExchangeStrategy
{
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.IsContainFood() || holder2.IsContainFood()) &&
            (!holder1.IsContainCookware() && !holder2.IsContainCookware());
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        holder1.SwapFoodTwoWay(holder2);
    }
}
