public class FoodInCookwareProcess : IFoodProcess<Cookware>
{
    public bool IsConvertible(Cookware heldItem)
    {
        return true;
    }

    public void ChangeFoodState(Cookware heldItem)
    {
    }
}