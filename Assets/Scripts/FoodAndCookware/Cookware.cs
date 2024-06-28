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

public class Cookware : PickUpAbtract
{
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
                CookwareRecipeController.Reset();
                return;
            }
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
            if(food == null)
            {
                Debug.Log("This food is null when checking can swap");
                return true;
            }
            var canPutFood = CanPutFood(food.GetData());
            Debug.Log(canPutFood ? "Can put food in plate " : "Cannot put food in plate");
            return canPutFood;
        }
        var CanSwapFoodInCookware = CookwareManager.instance.CanPutFoodInCookware(type, food);
        Debug.Log(CanSwapFoodInCookware ? "Can put food in normal cookware " : "Cannot put food in normal cookware ");
        return CanSwapFoodInCookware;
    }

    public bool CanPutFood(FoodData foodData)
    {
        if (CookwareRecipeController.IngredientQuantityCount == 0)
        {
            if (FoodManager.instance.IsFoodInRecipe(foodData, out var recipeMath) == false) return false;

            CookwareRecipeController.AddMatchListRecipe(recipeMath);
        }

        if (IsFoodInRecipeMatch(foodData)) return true;
        return false;
    }

    private bool IsFoodInRecipeMatch(FoodData foodData)
    {
        if (CookwareRecipeController.TotalRecipesCount == 0) return true;
        foreach (var currentRecipeStructure in CookwareRecipeController.GetRecipeList())
        {
            if (currentRecipeStructure.isComplete) continue;
            var count = CookwareRecipeController.GetCountOfFood(foodData);
            if (currentRecipeStructure.Recipes.IsContainWithCount(foodData, count))
            {
                Debug.Log("is contain food data: " + foodData.name);
                return true;
            }
        }

        // remove matchedRecipe of it not need
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