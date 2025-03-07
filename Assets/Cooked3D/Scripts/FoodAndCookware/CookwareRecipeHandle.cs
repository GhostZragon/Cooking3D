﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public partial class CookwareRecipeHandle : MonoBehaviour
{
    [SerializeField] private List<RecipeStructure> RecipeStructures;
    [SerializeField] private IngredientStockpile IngredientQuantitiesHandle;
    private List<Recipes> completeRecipesList = new List<Recipes>();

    private void Awake()
    {
        RecipeStructures = new List<RecipeStructure>();
        IngredientQuantitiesHandle = new IngredientStockpile();
    }

    public IngredientStockpile IngredientQuantitiesCollection => IngredientQuantitiesHandle;
    public int TotalRecipesCount => RecipeStructures.Count;
    public int IngredientQuantityCount => IngredientQuantitiesHandle.IngredientCount;
   
    /// <summary>
    /// Add list of recipe to valid future food
    /// </summary>
    /// <param name="recipesList"></param>
    public void AddMatchListRecipe(List<Recipes> recipesList)
    {
        bool temp = false;
        foreach (var recipe in recipesList)
        {
            
            Debug.Log(recipe.name);
            temp = true;
            foreach (var item in RecipeStructures)
            {
                if (recipe.Equals(item.Recipes))
                {
                    temp = false;
                    break;
                }
            }
            if (temp)
            {
                RecipeStructure recipeStructure;
                recipeStructure.Recipes = recipe;
                recipeStructure.isComplete = false;
                RecipeStructures.Add(recipeStructure);
            }
    
        }
    }

    public List<RecipeStructure> GetRecipeList()
    {
        return RecipeStructures;
    }
    /// <summary>
    /// Use
    /// </summary>
    /// <param name="foodData"></param>
    public void RefreshOrInsertFoodDetails(FoodData foodData)
    {
        if (IngredientQuantitiesHandle.ContainsFoodData(foodData))
            IngredientQuantitiesHandle.IncreaseCount(foodData, 1);
        else
            IngredientQuantitiesHandle.Add(foodData);
    }

    /// <summary>
    /// Use for process food
    /// </summary>
    /// <param name="foodData"></param>
    public void UpdateCurrentFood(FoodData newFoodData, FoodData oldFoodData)
    {
        IngredientQuantitiesHandle.UpdateOldFoodData(newFoodData, oldFoodData);
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
    /// <summary>
    /// Checking food data is inside valid list recipe
    /// </summary>
    /// <param name="foodData"></param>
    /// <returns></returns>
    public bool IsFoodInRecipeMatch(FoodData foodData)
    {
        if (TotalRecipesCount == 0) return false;
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
        for (int i = 0; i < RecipeStructures.Count; i++)
        {
            if (RecipeStructures[i].isComplete) continue;
            var matchedRecipe = RecipeStructures[i];
            var recipeDataSO = matchedRecipe.Recipes;
            var requiredIngredients = recipeDataSO.GetRequiredIngredients();

            if (requiredIngredients.IsEqual(IngredientQuantitiesCollection))
            {
                if (IngredientQuantityCount == requiredIngredients.IngredientCount)
                {
                    FoodInPlates.SetData(recipeDataSO.FoodResult);
                    FoodInPlates.SetModel();
                    // Directly update the struct in the list
                    matchedRecipe.CompleteFood();
                    RecipeStructures[i] = matchedRecipe; // Update the list with the modified struct
                    completeRecipesList.Add(matchedRecipe.Recipes);
                    Debug.Log("Complete food");
                }
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
                Debug.Log($"{transform.name} cannot combine: "+ cookwareRecipeHandle.name);
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