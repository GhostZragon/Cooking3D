using NaughtyAttributes;
using UnityEngine;

public class ProcressContainer : HolderAbstract
{
    [SerializeField] private FoodState foodStateWantToChange;

    [Button]
    private void Convert()
    {
        if (food == null) return; 
        var foodData = FoodManager.instance.GetFoodData(this.food.GetFoodType(),foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }
}
