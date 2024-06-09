public class SourcePlateContainer : BaseContainer<Plate>
{
    public override void ExchangeItems(HolderAbstract holder)
    {
        if (CanStopContinueSwap(holder)) return;
        holder.SetPlate(RetrieveRawFood());
    }
}