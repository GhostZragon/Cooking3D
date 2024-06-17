using UnityEngine;
public class SourcePlateContainer : BaseContainer<Cookware>
{
    public override void ExchangeItems(HolderAbstract player)
    {
        if (CanStopContinueSwap(player) == false) return;
        Debug.Log("Get plate");
        player.SetPlate(RetrieveRawFood());
    }
}
