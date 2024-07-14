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

    private event Action OnPlateDiscardCallback;

    private CookwareManager cookwareManager;
    private FoodManager foodManager;
    private void Awake()
    {
        CookwareRecipeController = GetComponent<CookwareRecipeHandle>();
    }
    private void Start()
    {
        cookwareManager = ServiceLocator.Current.Get<CookwareManager>();
        foodManager = ServiceLocator.Current.Get<FoodManager>();
    }

    public Food GetFood()
    {
        return FoodInPlates;
    }
    /// <summary>
    /// Add call back when plate is discard
    /// </summary>
    /// <param name="callback"></param>
    public void SetOnPlateDiscardCallback(Action callback)
    {
        OnPlateDiscardCallback = callback;
    }
    /// <summary>
    /// Add food to cookware
    /// </summary>
    /// <param name="food"></param>
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
        
    /// <summary>
    /// Add food to stock
    /// </summary>
    /// <param name="food"></param>
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
    /// <summary>
    /// Checking have any food data in stock
    /// </summary>
    /// <returns></returns>
    public bool IsContainFoodInPlate()
    {
        return CookwareRecipeController.IngredientQuantityCount > 0;
    }
    
    /// <summary>
    /// Use for general case for all cookware type
    /// </summary>
    /// <param name="food"></param>
    /// <param name="isContainByPlate"></param>
    /// <returns></returns>
    public bool CanSwapFood(Food food, bool isContainByPlate = false)
    {
        
        if (type == CookwareType.Plate) // if cookware is plate, need to check that food is valid to some recipe
        {
            if (food == null)
            {
                return true;
            }
            var canPutFood = CanPutFood(food.GetData());
            return canPutFood;
        }
        // checking that cookware can hold that food 
        var CanSwapFoodInCookware = cookwareManager.CanPutFoodInCookware(type, food);
        return CanSwapFoodInCookware || isContainByPlate;
    }
   
    /// <summary>
    /// Use for case need to check food can combine or add in (PLATE)
    /// </summary>
    /// <param name="foodData"></param>
    /// <returns></returns>
    public bool CanPutFood(FoodData foodData)
    {
        // if this plate is empty then create a list of recipe valid with that food data
        if (CookwareRecipeController.IngredientQuantityCount == 0)
        {
            if (foodManager.IsFoodInRecipe(foodData, out var recipeMath) == false)
            {
                Debug.Log($"{foodData.name} not match");
                return false;
            }

            CookwareRecipeController.AddMatchListRecipe(recipeMath);
        }

        if (CookwareRecipeController.IsFoodInRecipeMatch(foodData))
        {
            Debug.Log($"{foodData.name} match");
            return true;
        }
        Debug.Log($"{foodData.name} not match");
        return false;
    }


    public CookwareType GetCookwareType()
    {
        return type;
    }
    /// <summary>
    /// Update model of cookware
    /// </summary>
    /// <param name="newType"></param>
    public void SetCookwareType(CookwareType newType)
    {
        type = newType;
        foreach (var item in listModel) item.Model.SetActive(item.type == type);
    }

    public override void Discard()
    {
        DiscardFood();
        OnPlateDiscardCallback?.Invoke();
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