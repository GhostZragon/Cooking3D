using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable] 
public class IngredientQuantity
{
    public IngredientQuantity(FoodData foodData, int amout)
    {
        this.foodData = foodData;
        this.amount = amout;
    }
    [SerializeField] private FoodData foodData;
    [SerializeField] private int amount;
    public int Amount { get => amount; }
    public FoodData FoodData { get => foodData; }
    public bool CanAddMore(IngredientQuantity ingredientQuantity)
    {
        return ingredientQuantity.amount < amount;
    }

    public void SetFoodData(FoodData foodData)
    {
        this.foodData = foodData;
    }
    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
    
}
[CreateAssetMenu(fileName = "RecipeStructure", menuName = "ScriptableObjects/RecipeStructure")]
public class Recipes : ScriptableObject
{
    //[SerializeField,Expandable] private List<FoodData> foodNeed;
    [SerializeField, Expandable] private FoodData foodResult;
    [SerializeField] private IngredientStockpile recipeIngredient;
    public bool IsContain(FoodData foodDataNeedToCheck)
    {
        if (foodDataNeedToCheck == null) return false;
        return recipeIngredient.ContainsFoodData(foodDataNeedToCheck);
    }
    public bool IsContainWithCount(FoodData foodDataNeedToCheck,int count)
    {
        return recipeIngredient.ContainsFoodDataWithCount(foodDataNeedToCheck, count);
    }
    public IngredientStockpile GetRequiredIngredients()
    {
        return recipeIngredient;
    }

    //public bool IsValid(List<FoodData> foodDatas)
    //{
    //    if (foodDatas.TotalRecipesCount != foodNeed.TotalRecipesCount) return false;
    //    foreach(var food in foodNeed)
    //    {
    //        if (!foodDatas.ContainsFoodDataWithCount(food)) return false;
    //    }
    //    return true;
    //}
    public FoodData FoodResult
    {
        get => foodResult;
        set => foodResult = value;
    }
   
}