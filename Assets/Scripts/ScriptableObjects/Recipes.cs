using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable] 
public struct RecipeFood
{
    public FoodData foodData;
    public int amount;
}
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipes : ScriptableObject
{
    [SerializeField,Expandable] private List<FoodData> foodNeed;
    [SerializeField, Expandable] private FoodData foodResult;
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

    public int GetFoodCount()
    {
        return foodNeed.Count;
    }

    public List<FoodData> GetFoodList()
    {
        return foodNeed;
    }

    //public bool IsValid(List<FoodData> foodDatas)
    //{
    //    if (foodDatas.Count != foodNeed.Count) return false;
    //    foreach(var food in foodNeed)
    //    {
    //        if (!foodDatas.Contains(food)) return false;
    //    }
    //    return true;
    //}
    public FoodData FoodResult
    {
        get => foodResult;
        set => foodResult = value;
    }
}