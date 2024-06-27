using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;
    private const string mat_Name = "restaurantbits_mat";

    [SerializeField] private FoodDatabase FoodDatabase;
    [SerializeField] private Food foodPrefab;
    [SerializeField] private Recipes Recipes;
    [SerializeField] private Material food_mat;


    private void Awake()
    {
        instance = this;
    }

    private void OnValidate()
    {
        if (food_mat == null)
        {
            Debug.Log(mat_Name);
            food_mat = Resources.Load<Material>(ConstantPath.Resource.COOKING_MATERIAL);
        }
        if (foodPrefab == null)
        {
            Debug.LogWarning("Food uiItemPrefab is null", gameObject);
        }
    }

    public Food GetFoodInstantiate(FoodType foodType, FoodState foodState)
    {
        return InitFoodState(FoodDatabase.GetFoodData(foodState, foodType));
    }

    private Food InitFoodState(FoodData foodData)
    {
        var food = Instantiate(foodPrefab);
        food.SetData(foodData);
        var skin = food.GetComponent<FoodCustomizeMesh>();
        skin.SetMaterial(food_mat);
        skin.SetMesh(foodData.GetMesh());
        return food;
    }
    public bool CheckFoodValidToChange(Food food, FoodState foodState)
    {
        if (FoodDatabase == null)
        {
            Debug.Log("Food Database is null", gameObject);
            return true;
        }

        return FoodDatabase.CanTransitionToFoodState(food, foodState);
    }

    public FoodData GetFoodData(FoodType foodType, FoodState foodState)
    {
        return FoodDatabase.GetFoodData(foodState, foodType);
    }
    //public bool CanCombineFoodInCookware(Food foodWantCombine, Cookware cookwareNeedAdd, out FoodData foodData)
    //{
    //    listOnTest = new List<FoodData>(cookwareNeedAdd.GetContainedFoodData())
    //    {
    //        foodWantCombine.GetData()
    //    };
    //    foodData = CheckingListFoodIsValid(listOnTest, out bool isMathRecipe);


    //    return isMathRecipe;
    //}
    //public bool CanCombineFoodInCookware(Cookware cookware1, Cookware cookware2, out FoodData foodData)
    //{
    //    listOnTest = new List<FoodData>(cookware1.GetContainedFoodData());
    //    listOnTest.AddRange(new List<FoodData>(cookware1.GetContainedFoodData()));
    //    foodData = CheckingListFoodIsValid(listOnTest, out bool isMathRecipe);
    //    return isMathRecipe;
    //}
    //[Button]
    //private void Test()
    //{
    //    listOnTest.Clear();
    //}

    public List<Recipes> RecipesList;
    //public List<FoodData> listOnTest;
    //private FoodData CheckingListFoodIsValid(List<FoodData> listFoodData, out bool IsMathRecipe)
    //{
    //    IsMathRecipe = false;
    //    foreach (var recipe in RecipesList)
    //    {
    //        var itemCount = recipe.GetFoodCount();
    //        int notMatchItem = 0;
    //        var list = recipe.GetFoodList();
    //        foreach(var item in listFoodData)
    //        {
    //            if (!list.Contains(item))
    //            {
    //                break;
    //            }
    //        }
    //        if(notMatchItem == 0)
    //        {
    //            Debug.Log("math some of food");
    //            IsMathRecipe = true;

    //            if (listFoodData.Count == list.Count)
    //            {
    //                return recipe.FoodResult;
    //            }
    //            return null;
    //        }
    //    }
    //    return null;
    //}

    /// <summary>
    /// Checking all recipe of database
    /// </summary>
    /// <param name="food"></param>
    /// <returns></returns>
    public bool IsFoodInRecipe(Food food, out List<Recipes> listRecipeValid)
    {
        listRecipeValid = new List<Recipes>();
        var foodData = food.GetData();
        foreach (var item in RecipesList)
        {
            if (item.IsContain(foodData))
            {
                listRecipeValid.Add(item);
            }
        }
        return listRecipeValid.Count != 0;
    }
}
