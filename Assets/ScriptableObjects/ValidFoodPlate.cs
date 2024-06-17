using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ValidFoodPlate", menuName = "ValidFoodCookware/Plate")]
public class ValidFoodPlate : HolderValidFood
{
    public List<FoodState> ValidFoodStateList;
    public List<FoodData> ExceptionList;

    public override bool CheckingFoodIsValid(FoodData foodData)
    {
        if (ListChecking(foodData) == false)
        {
            if (ValidFoodStateList.Contains(foodData.FoodState))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        return true;
    }

    private bool ListChecking(FoodData foodData)
    {
        if (ExceptionList.Count == 0 || !ExceptionList.Contains(foodData)) return false;
        return true;
    }
}