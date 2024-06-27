using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CookwareRecipeHandle
{
    [Serializable]
    public struct RecipeStructure
    {
        public Recipes Recipes;
        public bool isComplete;

        public void CompleteFood()
        {
            isComplete = true;
        }
    }
    [SerializeField] private List<RecipeStructure> RecipeStructures;
    [SerializeField] private IngredientStockpile IngredientQuantities;
    public IngredientStockpile IngredientQuantitiesCollection => IngredientQuantities;
    public CookwareRecipeHandle()
    {
        RecipeStructures = new List<RecipeStructure>();
        IngredientQuantities = new IngredientStockpile();
    }
    public int TotalRecipesCount { get => RecipeStructures.Count; }
    public int IngredientQuantityCount { get => IngredientQuantities.IngredientCount;}

    public void AddMatchListRecipe(List<Recipes> recipesList)
    {
        foreach (var recipe in recipesList)
        {
            Debug.Log(recipe.name);
            var recipeStructure = new RecipeStructure();
            recipeStructure.Recipes = recipe;
            recipeStructure.isComplete = false;
            RecipeStructures.Add(recipeStructure);
        }
    }

    public List<RecipeStructure> GetRecipeList()
    {
        return RecipeStructures;
    }

    public void RefreshOrInsertFoodDetails(FoodData foodData)
    {
        if (IngredientQuantities.ContainsFoodData(foodData))
        {
            IngredientQuantities.IncreaseCount(foodData,1);
        }
        else
        {
            IngredientQuantities.Add(foodData);
        }
    }

    public void SetInitialFoodData(FoodData foodData)
    {
        IngredientQuantities.SetFirstIngredientFoodData(foodData);
    }

    public void Reset()
    {
        RecipeStructures.Clear();
        IngredientQuantities.ResetIngredientQuantities();
    }

    public int GetCountOfFood(FoodData foodData)
    {
        return IngredientQuantities.GetCountOfFoodData(foodData);
    }
}
