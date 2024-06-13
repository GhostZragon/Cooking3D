using UnityEngine;

public class CustomerContainer : MonoBehaviour , IHolder
{
    public void ExchangeItems(HolderAbstract holder)
    {
        if (holder.IsContainFoodInCookware() && 
            holder.GetCookware().GetCookwareType() == CookwareType.Plate)
        {

        }
    }
}