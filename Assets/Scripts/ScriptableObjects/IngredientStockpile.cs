using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class IngredientStockpile
{
    [SerializeField] private List<IngredientQuantity> ingredientQuantities;
    public IngredientStockpile()
    {
        ingredientQuantities = new List<IngredientQuantity>();
    }
    public int GetCountOfFoodData(FoodData foodData)
    {
        foreach(var item in ingredientQuantities)
        {
            if (item.FoodData == foodData)
                return item.Amount;
        }
        return 0;
    }
    public int IngredientCount { get => ingredientQuantities.Count; }
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
        IngredientQuantity IngredientQuantity = new IngredientQuantity(foodData,1);
        ingredientQuantities.Add(IngredientQuantity);
    }

    public void ResetIngredientQuantities()
    {
        ingredientQuantities.Clear();
    }

    public void IncreaseCount(FoodData foodData, int v)
    {
        foreach (var item in ingredientQuantities)
        {
            if (item.FoodData == foodData)
            {
                item.SetAmount(item.Amount + v);
            }
        }
    }

    public bool IsEqual(IngredientStockpile ingredientQuantitiesCollection)
    {
        if (ingredientQuantitiesCollection.IngredientCount > IngredientCount)
        {
            Debug.Log("Fall");
            return false;
        }
        bool HasAllNecessaryIngredients = IncludesAllRequiredIngredients(ingredientQuantitiesCollection);
        if (HasAllNecessaryIngredients)
        {
            Debug.Log("Contain food");
        }
        else
        {
            Debug.Log("Not contain food");
        }
        return HasAllNecessaryIngredients;
    }
    private bool IncludesAllRequiredIngredients(IngredientStockpile ingredientQuantitiesCollection)
    {
        foreach (var item in ingredientQuantitiesCollection.ingredientQuantities)
        {
            if (!ContainsExactFoodDataCount(item.FoodData, item.Amount))
                return false;
        }
        return true;
    }
    private bool ContainsExactFoodDataCount(FoodData foodData, int count)
    {
        bool isFound = false;
        foreach (var item in ingredientQuantities)
        {
            if (item.FoodData == foodData && item.Amount == count)
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
}
