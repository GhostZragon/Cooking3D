using System;
using System.Collections.Generic;
using UnityEngine;

public enum CookwareType
{
    None,
    Plate,
    Pot,
    Pan
}
[RequireComponent(typeof(CookwareRecipeHandle))]
public class Cookware : PickUpAbtract
{
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;
    [SerializeField] private List<CookwareModel> listModel;
    [SerializeField] private CookwareRecipeHandle CookwareRecipeController;

    private CookwareManager cookwareManager;
    private FoodManager foodManager;
    private void Awake()
    {
        CookwareRecipeController = GetComponent<CookwareRecipeHandle>();
    }
    private void Start()
    {
        cookwareManager = CookwareManager.instance;
        foodManager = FoodManager.instance;
    }
    public Food GetFood()
    {
        return FoodInPlates;
    }

    public void Swap(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        if (food != null)
        {
            food.SetToParentAndPosition(PlaceTransform);
        }
        FoodInPlates = food;

        HandleFoodAddition(food);
    }

    private void HandleFoodAddition(Food food)
    {
        if (CookwareRecipeController.IngredientQuantityCount == 0)
            CookwareRecipeController.RefreshOrInsertFoodDetails(food.GetData());
        else
        {
            if(food == null)
            {
                CookwareRecipeController.ResetData();
                return;
            }
            CookwareRecipeController.SetInitialFoodData(food.GetData());
        }
    }

    public void DiscardFood()
    {
        if(FoodInPlates != null)
        {
            FoodInPlates.Discard();
        }

        CookwareRecipeController.ResetData();
    }

    public bool IsContainFoodInPlate()
    {
        return CookwareRecipeController.IngredientQuantityCount > 0;
    }

    public bool CanSwapFood(Food food)
    {
        if (type == CookwareType.Plate)
        {
            if(food == null)
            {
                return true;
            }
            var canPutFood = CanPutFood(food.GetData());
            return canPutFood;
        }
        var CanSwapFoodInCookware = cookwareManager.CanPutFoodInCookware(type, food);
        return CanSwapFoodInCookware;
    }

    public bool CanPutFood(FoodData foodData)
    {
        if (CookwareRecipeController.IngredientQuantityCount == 0)
        {
            if (foodManager.IsFoodInRecipe(foodData, out var recipeMath) == false) return false;

            CookwareRecipeController.AddMatchListRecipe(recipeMath);
        }

        if (CookwareRecipeController.IsFoodInRecipeMatch(foodData)) return true;
        return false;
    }


    public CookwareType GetCookwareType()
    {
        return type;
    }

    public void SetCookwareType(CookwareType newType)
    {
        type = newType;
        foreach (var item in listModel) item.Model.SetActive(item.type == type);
    }

    public override void Discard()
    {
        DiscardFood();
        Destroy(gameObject);
    }

    public void CombineFood(FoodData foodData)
    {
        CookwareRecipeController.RefreshOrInsertFoodDetails(foodData);
        var cookwareRecipes = CookwareRecipeController.GetRecipeList();
        // cheking is math all food in recipeData
        foreach (var matchedRecipe in cookwareRecipes)
        {
            if (matchedRecipe.isComplete) continue;
            var recipeDataSO = matchedRecipe.Recipes;
            var requiredIngredients = recipeDataSO.GetRequiredIngredients();

            if (requiredIngredients.IsEqual(CookwareRecipeController.IngredientQuantitiesCollection))
                if (CookwareRecipeController.IngredientQuantityCount == requiredIngredients.IngredientCount)
                {
                    FoodInPlates.SetData(recipeDataSO.FoodResult);
                    FoodInPlates.SetModel();
                    matchedRecipe.CompleteFood();
                }
        }
    }

    public bool CanCombineWithCookware(Cookware cookware2)
    {
        if (IsContainFoodInPlate() && cookware2.IsContainFoodInPlate())
        {
            // check can combine here
            bool canCombine = true;
            // check food in cookware 2 can put in cookware 1 ?
            Debug.Log("On check 2 cookware");
            foreach(var ingredientData in cookware2.CookwareRecipeController.GetCurrentFoodDataList())
            {
                if (!CanPutFood(ingredientData.FoodData))
                {
                    Debug.Log("It cannot combine");
                    canCombine = false;
                    break;
                }
            }
            if (canCombine)
            {
                foreach (var ingredientData in cookware2.CookwareRecipeController.GetCurrentFoodDataList())
                {
                    CombineFood(ingredientData.FoodData);
                }
            }

            return canCombine;
        }
        return false;
    }

    [Serializable]
    public struct CookwareModel
    {
        public CookwareType type;
        public GameObject Model;
    }
}