public class SourcePlateContainer : BaseContainer<Plate>
{
    public override void ExchangeItems(HolderAbstract holder)
    {
        if (holder.IsContainFood() || holder.IsContainPlate()) return;
        holder.SetPlate(RetrieveRawFood());
    }
}