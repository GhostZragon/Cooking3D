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
            if (food == null)
            {
                CookwareRecipeController.ResetData();
                return;
            }
            CookwareRecipeController.SetInitialFoodData(food.GetData());
        }
    }

    public void DiscardFood()
    {
        if (FoodInPlates != null)
        {
            FoodInPlates.Discard();
        }

        CookwareRecipeController.ResetData();
    }

    public bool IsContainFoodInPlate()
    {
        return CookwareRecipeController.IngredientQuantityCount > 0;
    }

    public bool CanSwapFood(Food food, bool isContainByPlate = false)
    {
        if (type == CookwareType.Plate)
        {
            if (food == null)
            {
                return true;
            }
            var canPutFood = CanPutFood(food.GetData());
            return canPutFood;
        }
        var CanSwapFoodInCookware = cookwareManager.CanPutFoodInCookware(type, food);
        return CanSwapFoodInCookware || isContainByPlate;
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
        CookwareRecipeController.CombineFood(foodData, FoodInPlates);
    }

    public bool CanCombineWithCookware(Cookware cookware2)
    {
        if (!IsContainFoodInPlate() || !cookware2.IsContainFoodInPlate()) return false;
 
        Debug.Log("On check 2 cookware");
   

        return CookwareRecipeController.CombineFood(cookware2.CookwareRecipeController, CanPutFood, FoodInPlates);
    }

    [Serializable]
    public struct CookwareModel
    {
        public CookwareType type;
        public GameObject Model;
    }
}