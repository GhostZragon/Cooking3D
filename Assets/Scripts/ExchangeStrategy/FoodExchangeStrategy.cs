/// <summary>
/// Condition:
/// Holder 1 or Holder2 containd Food
/// Holder 1 or Holder2 not have cookware
/// Swap:
/// Normaly swap food 
/// </summary>
public class ExchangeFoodPattern : IExchangeStrategy
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
