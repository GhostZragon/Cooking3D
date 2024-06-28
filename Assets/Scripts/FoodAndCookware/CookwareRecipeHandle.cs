using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CookwareRecipeHandle
{
    [SerializeField] private List<RecipeStructure> RecipeStructures;
    [SerializeField] private IngredientStockpile IngredientQuantitiesHandle;

    public CookwareRecipeHandle()
    {
        RecipeStructures = new List<RecipeStructure>();
        IngredientQuantitiesHandle = new IngredientStockpile();
    }

    public IngredientStockpile IngredientQuantitiesCollection => IngredientQuantitiesHandle;
    public int TotalRecipesCount => RecipeStructures.Count;
    public int IngredientQuantityCount => IngredientQuantitiesHandle.IngredientCount;

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
        if (IngredientQuantitiesHandle.ContainsFoodData(foodData))
            IngredientQuantitiesHandle.IncreaseCount(foodData, 1);
        else
            IngredientQuantitiesHandle.Add(foodData);
    }

    public void SetInitialFoodData(FoodData foodData)
    {
        IngredientQuantitiesHandle.SetFirstIngredientFoodData(foodData);
    }

    public void Reset()
    {
        RecipeStructures.Clear();
        IngredientQuantitiesHandle.ResetIngredientQuantities();
    }

    public int GetCountOfFood(FoodData foodData)
    {
        return IngredientQuantitiesHandle.GetCountOfFoodData(foodData);
    }

    public List<IngredientQuantity> GetCurrentFoodDataList()
    {
        return IngredientQuantitiesCollection.GetIngredientQuantities();
    }

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
}