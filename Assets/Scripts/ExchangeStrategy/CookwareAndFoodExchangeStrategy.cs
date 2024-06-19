using UnityEngine;

public partial class CookwareAndFoodExchangeStrategy : IExchangeStrategy
{
    private IExchangeStrategy subExchangeStrategy;
    public CookwareAndFoodExchangeStrategy()
    {
        subExchangeStrategy = new CookwareFoodAndFoodExchangeStragety();
    }
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.IsContainCookware() || holder2.IsContainCookware()) &&
            (holder1.IsContainFood() == false || holder2.IsContainFood()== false);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {

        if (subExchangeStrategy.CanExchange(holder1,holder2))
        {
            subExchangeStrategy.Exchange(holder1, holder2);
        }
        else
        {
            // need to swap targetFood here
            Debug.Log("put targetFood in targetCookware");
            holder1.SwapFoodAndCookwareOneWay(holder2);
        }
    }
}
