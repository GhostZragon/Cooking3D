using UnityEngine;

public class CustomerContainer : MonoBehaviour , IHolder
{
    public void ExchangeItems(HolderAbstract holder)
    {
        // if (holder == null)
        // {
        //     Debug.Log("jja");
        // }
        // var plate = holder.GetPlate();
        // if (plate == null || plate.GetComponent<Cookware>().IsContainFoodInPlate() == false) return;
        // plate.Delete();
        // holder.SetPlate(null);
    }
}