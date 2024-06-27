using UnityEngine;

public partial class ExchangeCookwarePattern : IExchangeStrategy
{
    private IExchangeStrategy subExchangeStrategy;
    public ExchangeCookwarePattern()
    {
        subExchangeStrategy = new CookwareHaveFoodPattern();
    }
    public bool CanExchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        return (holder1.IsContainCookware() || holder2.IsContainCookware()) &&
            (holder1.IsContainFood() == false && holder2.IsContainFood() == false);
    }

    public void Exchange(HolderAbstract holder1, HolderAbstract holder2)
    {
        var cookware1 = holder1.GetCookware();
        var cookware2 = holder2.GetCookware();
        if (cookware1 == null || cookware2 == null ||(cookware1.IsContainFoodInPlate() == false && cookware2.IsContainFoodInPlate() == false))
        {
            //Debug.Log("no targetFood in targetCookware, and just need swap it");
            holder1.SwapCookwareTwoWay(holder2);
            return;
        }
        // Scenario 1
        if (subExchangeStrategy.CanExchange(holder1,holder2))
        {
            subExchangeStrategy.Exchange(holder1, holder2);
            Debug.Log("Swap targetFood in both targetCookware");
            
        }
    }
}
