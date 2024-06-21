using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipes : ScriptableObject
{
    [SerializeField] private List<FoodData> foodNeed;

    public bool IsContain(FoodData foodDataNeedToCheck)
    {
        if (foodDataNeedToCheck == null) return false;
        foreach(var food in foodNeed)
        {
            if (food == foodDataNeedToCheck)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsValid(List<FoodData> foodDatas)
    {
        if (foodDatas.Count != foodNeed.Count) return false;
        foreach(var food in foodNeed)
        {
            if (!foodDatas.Contains(food)) return false;
        }
        return true;
    }
}