using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class IngredientStockpile
{
    [SerializeField] private List<IngredientQuantity> ingredientQuantities = new();
    private Dictionary<FoodData, IngredientQuantity> ingredientQuantityDict;

    public IngredientStockpile()
    {
        ingredientQuantityDict = ingredientQuantities.ToDictionary(iq => iq.FoodData);
    }

    public int GetCountOfFoodData(FoodData foodData)
    {
        return ingredientQuantityDict.TryGetValue(foodData, out var ingredientQuantity) ? ingredientQuantity.Amount : 0;
    }

    public int IngredientCount => ingredientQuantities.Count;

    public bool ContainsFoodDataWithCount(FoodData foodData, int currentCount)
    {
        return ingredientQuantityDict.TryGetValue(foodData, out var ingredientQuantity) && currentCount < ingredientQuantity.Amount;
    }

    public bool ContainsFoodData(FoodData foodData)
    {
        return ingredientQuantityDict.ContainsKey(foodData);
    }

    public void SetFirstIngredientFoodData(FoodData foodData)
    {
        if (ingredientQuantities.Count > 0)
        {
            ingredientQuantities[0].SetFoodData(foodData);
            ingredientQuantityDict[foodData] = ingredientQuantities[0];
        }
    }

    public void Add(FoodData foodData)
    {
        var ingredientQuantity = new IngredientQuantity(foodData, 1);
        ingredientQuantities.Add(ingredientQuantity);
        ingredientQuantityDict[foodData] = ingredientQuantity;
    }

    public void ResetIngredientQuantities()
    {
        ingredientQuantities.Clear();
        ingredientQuantityDict.Clear();
    }

    public void IncreaseCount(FoodData foodData, int v)
    {
        if (ingredientQuantityDict.TryGetValue(foodData, out var ingredientQuantity))
        {
            ingredientQuantity.SetAmount(ingredientQuantity.Amount + v);
        }
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
        return ingredientQuantityDict.TryGetValue(foodData, out var ingredientQuantity) && ingredientQuantity.Amount == count;
    }

    public List<IngredientQuantity> GetIngredientQuantities()
    {
        return ingredientQuantities;
    }

    public void UpdateOldFoodData(FoodData newFoodData, FoodData oldFoodData)
    {
        if (ingredientQuantityDict.TryGetValue(oldFoodData, out var ingredientQuantity))
        {
            ingredientQuantity.SetFoodData(newFoodData);
            ingredientQuantityDict.Remove(oldFoodData);
            ingredientQuantityDict[newFoodData] = ingredientQuantity;
        }
    }
}
