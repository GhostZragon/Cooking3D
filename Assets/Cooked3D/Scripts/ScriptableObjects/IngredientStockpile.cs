using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class IngredientStockpile
{
    [SerializeField] private List<IngredientQuantity> ingredientQuantities = new();

    public int GetCountOfFoodData(FoodData foodData)
    {
        foreach (var item in ingredientQuantities)
            if (item.FoodData == foodData)
                return item.Amount;

        return 0;
    }

    public int IngredientCount => ingredientQuantities.Count;

    public bool ContainsFoodDataWithCount(FoodData foodData, int currentCount)
    {
        return ingredientQuantities.Any(item => item.FoodData == foodData && currentCount < item.Amount);
    }

    public bool ContainsFoodData(FoodData foodData)
    {
        return ingredientQuantities.Any(item => item.FoodData == foodData);
    }

    public void SetFirstIngredientFoodData(FoodData foodData)
    {
        ingredientQuantities[0].SetFoodData(foodData);
    }

    public void Add(FoodData foodData)
    {
        var ingredientQuantity = new IngredientQuantity(foodData, 1);
        ingredientQuantities.Add(ingredientQuantity);
    }

    public void ResetIngredientQuantities()
    {
        ingredientQuantities.Clear();
    }

    public void IncreaseCount(FoodData foodData, int v)
    {
        foreach (var item in ingredientQuantities)
            if (item.FoodData == foodData)
                item.SetAmount(item.Amount + v);
    }

    public bool IsEqual(IngredientStockpile ingredientQuantitiesCollection)
    {
        if (ingredientQuantitiesCollection.IngredientCount > IngredientCount)
        {
            Debug.Log("Fall");
            return false;
        }

        var hasAllNecessaryIngredients = IncludesAllRequiredIngredients(ingredientQuantitiesCollection);
        Debug.Log(hasAllNecessaryIngredients ? "Contain food" : "Not contain food");
        return hasAllNecessaryIngredients;
    }

    private bool IncludesAllRequiredIngredients(IngredientStockpile ingredientQuantitiesCollection)
    {
        return ingredientQuantitiesCollection.ingredientQuantities.All(item =>
            ContainsExactFoodDataCount(item.FoodData, item.Amount));
    }

    private bool ContainsExactFoodDataCount(FoodData foodData, int count)
    {
        var isFound = false;
        foreach (var item in ingredientQuantities)
            if (Equals(item.FoodData, foodData) && item.Amount == count)
            {
                isFound = true;
                break;
            }

        return isFound;
    }
    public List<IngredientQuantity> GetIngredientQuantities()
    {
        return this.ingredientQuantities;
    }

    public void UpdateOldFoodData(FoodData newFoodData, FoodData oldFoodData)
    {
        foreach(var food in ingredientQuantities)
        {
            if(food.FoodData == oldFoodData)
            {
                food.SetFoodData(newFoodData);
                break;
            }
        }
    }
}