using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipes : ScriptableObject
{
    [SerializeField] private List<FoodData> foodNeed;
    [SerializeField] private FoodData foodResult;
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
    public FoodData FoodResult
    {
        get => foodResult;
        set => foodResult = value;
    }
}