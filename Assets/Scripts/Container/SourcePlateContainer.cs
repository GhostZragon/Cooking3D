using UnityEngine;
public class SourcePlateContainer : BaseContainer<Cookware>
{
    public override void ExchangeItems(HolderAbstract holder)
    {
        if (CanStopContinueSwap(holder) == false) return;
        Debug.Log("Get plate");
        holder.SetPlate(RetrieveRawFood());
    }
}
