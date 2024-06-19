using UnityEngine;

public class CustomerContainer : MonoBehaviour , IHolder
{
    public void ExchangeItems(HolderAbstract player)
    {
        if (player.IsContainFoodInCookware() && 
            player.GetCookwareType() == CookwareType.Plate)
        {
            Debug.Log("Exchange");
        }
    }
}