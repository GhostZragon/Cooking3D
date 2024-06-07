using UnityEngine;

public class CustomerContainer : MonoBehaviour , IHolder
{
    public void ExchangeItems(HolderAbstract holder)
    {
        if (holder == null)
        {
            Debug.Log("jja");
        }
        var plate = holder.GetPlate();
        if (plate == null || plate.GetComponent<Plate>().HasFoodInPlate() == false) return;
        Debug.Log("You can put food to customer");
        Destroy(plate.gameObject);
        holder.SetPlate(null);
    }
}