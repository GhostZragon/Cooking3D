public class FoodProcess : IFoodProcess<Food>
{
    public FoodState foodStateWantToChange;

    public FoodProcess(FoodState newFoodState)
    {
        foodStateWantToChange = newFoodState;
    }

    public void ChangeFoodState(Food food)
    {
        var foodData = FoodManager.instance.GetFoodData(food.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }

    public bool IsConvertible(Food food)
    {
        var foodNotSameState = food.GetFoodState() != foodStateWantToChange;
        var isFoodStateInDatabase = FoodManager.instance.CheckFoodValidToChange(food, foodStateWantToChange);
        return foodNotSameState && isFoodStateInDatabase;
    }
}