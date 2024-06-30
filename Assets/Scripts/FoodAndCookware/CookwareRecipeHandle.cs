using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class CookwareRecipeHandle : MonoBehaviour
{
    [SerializeField] private List<RecipeStructure> RecipeStructures;
    [SerializeField] private IngredientStockpile IngredientQuantitiesHandle;

    private void Awake()
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

    public void ResetData()
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
    public bool IsFoodInRecipeMatch(FoodData foodData)
    {
        if (TotalRecipesCount == 0) return true;
        foreach (var currentRecipeStructure in RecipeStructures)
        {
            if (currentRecipeStructure.isComplete) continue;
            var count = GetCountOfFood(foodData);
            if (currentRecipeStructure.Recipes.IsContainWithCount(foodData, count))
            {
                Debug.Log("is contain food data: " + foodData.name);
                return true;
            }
        }
        return false;
    }
    public void CombineFood(FoodData foodData, Food FoodInPlates)
    {
        RefreshOrInsertFoodDetails(foodData);
        // cheking is math all food in recipeData
        foreach (var matchedRecipe in RecipeStructures)
        {
            if (matchedRecipe.isComplete) continue;
            var recipeDataSO = matchedRecipe.Recipes;
            var requiredIngredients = recipeDataSO.GetRequiredIngredients();

            if (requiredIngredients.IsEqual(IngredientQuantitiesCollection))
                if (IngredientQuantityCount == requiredIngredients.IngredientCount)
                {
                    FoodInPlates.SetData(recipeDataSO.FoodResult);
                    FoodInPlates.SetModel();
                    matchedRecipe.CompleteFood();
                }
        }
    }
    public bool CombineFood(CookwareRecipeHandle cookwareRecipeHandle, Func<FoodData,bool> CanPutFood,Food foodInPlate)
    {
        bool canCombine = true;
        foreach (var ingredientData in cookwareRecipeHandle.GetCurrentFoodDataList())
        {

            if (!CanPutFood(ingredientData.FoodData))
            {
                Debug.Log("It cannot combine");
                canCombine = false;
                break;
            }

        }
        // If all food data can combine, then combine it
        if (canCombine)
        {
            // add each food data to this cookware 
            foreach (var ingredientData in cookwareRecipeHandle.GetCurrentFoodDataList())
            {
                CombineFood(ingredientData.FoodData, foodInPlate);
            }

        }

        return canCombine;
    }
}