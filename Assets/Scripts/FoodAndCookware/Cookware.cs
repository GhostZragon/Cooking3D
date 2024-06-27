using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum CookwareType
{
    None,
    Plate,
    Pot,
    Pan,
}
public class Cookware : PickUpAbtract
{
    [Serializable]
    public struct CookwareModel
    {
        public CookwareType type;
        public GameObject Model;
    }
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;
    [SerializeField] private List<CookwareModel> listModel;


    [SerializeField] private CookwareRecipeHandle CookwareRecipeController;

    private void Awake()
    {
        CookwareRecipeController = new CookwareRecipeHandle();
    }

    public Food GetFood()
    {
        return FoodInPlates;
    }

    public void Swap(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food?.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;

        HandleFoodAddition(food);

    }

    private void HandleFoodAddition(Food food)
    {
        if (CookwareRecipeController.IngredientQuantityCount == 0)
        {
            CookwareRecipeController.RefreshOrInsertFoodDetails(food.GetData());
        }
        else
        {
            //foodDatas[0] = food.GetData();
            CookwareRecipeController.SetInitialFoodData(food.GetData());
        }
    }

    public void DiscardFood()
    {
        Destroy(FoodInPlates.gameObject);

        CookwareRecipeController.Reset();
    }

    public bool IsContainFoodInPlate()
    {
        return CookwareRecipeController.IngredientQuantityCount > 0;
    }

    public bool CanSwapFood(Food food)
    {
        Debug.LogWarning("IF have process food need multiple item, PLS Upgrade this check");
        if (type == CookwareType.Plate)
        {
            return CanPutFood(food);
        }
        else
        {
            return CookwareManager.instance.CanPutFoodInCookware(type, food);
        }
    }
    public bool CanPutFood(Food food)
    {
        if (CookwareRecipeController.IngredientQuantityCount == 0)
        {
            if (FoodManager.instance.IsFoodInRecipe(food, out var recipeMath) == false)
            {
                return false;
            }
            CookwareRecipeController.AddMatchListRecipe(recipeMath);
        }
        if (IsFoodInRecipeMatch(food)) return true;
        return false;
    }
    private bool IsFoodInRecipeMatch(Food food)
    {
        if (CookwareRecipeController.TotalRecipesCount == 0) return true;
        var foodData = food.GetData();
        foreach (var currentRecipeStructure in CookwareRecipeController.GetRecipeList())
        {
            if (currentRecipeStructure.isComplete) continue;
            var count = CookwareRecipeController.GetCountOfFood(foodData);
            if (currentRecipeStructure.Recipes.IsContainWithCount(foodData,count))
            {
                Debug.Log("is contain food data: " + foodData.name);
                return true;
            }
        }

        // remove matchedRecipe of it not need
        return false;
    }
    public CookwareType GetCookwareType() => type;
    public void SetCookwareType(CookwareType newType)
    {
        type = newType;
        foreach (var item in listModel)
        {
            item.Model.SetActive(item.type == type);
        }
    }

    public override void Discard()
    {
    }

    public void CombineFood(Food targetFood)
    {
        CookwareRecipeController.RefreshOrInsertFoodDetails(targetFood.GetData());
        var cookwareRecipes = CookwareRecipeController.GetRecipeList();
        // cheking is math all food in recipeData
        foreach (var matchedRecipe in cookwareRecipes)
        {
            if (matchedRecipe.isComplete == true) continue;
            var recipeDataSO = matchedRecipe.Recipes;
            var requiredIngredients = recipeDataSO.GetRequiredIngredients();
            
            if (requiredIngredients.IsEqual(CookwareRecipeController.IngredientQuantitiesCollection))
            {

                if (CookwareRecipeController.IngredientQuantityCount == requiredIngredients.IngredientCount)
                {
                    FoodInPlates.SetData(recipeDataSO.FoodResult);
                    FoodInPlates.SetModel();
                    matchedRecipe.CompleteFood();
                }
            }
   
        }
    }
  
}