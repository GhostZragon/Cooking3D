
using UnityEngine;

public class FoodProcess : BaseProcess<Food,FoodState>
{
    private FoodManager foodManager;
    public FoodProcess()
    {
        foodManager = ServiceLocator.Current.Get<FoodManager>();
    }
    public override void ApplyFoodStateChange(Food food, FoodState foodStateWantToChange)
    {
        var foodData = foodManager.GetFoodData(food.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }

    public override bool CanChangeFoodState(Food food, FoodState foodStateWantToChange)
    {
        if (food == null) return false;
        var foodNotSameState = food.GetFoodState() != foodStateWantToChange;
        var isFoodStateInDatabase = foodManager.CheckFoodValidToChange(food, foodStateWantToChange);
        Debug.Log($"foodNotSameState{foodNotSameState} isFoodStateInDatabase{isFoodStateInDatabase}");

        return foodNotSameState && isFoodStateInDatabase;
    }
}