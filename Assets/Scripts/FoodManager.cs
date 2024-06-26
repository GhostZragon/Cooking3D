using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(foodPrefab == null)
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
    public bool CanConvertFood(Food food, FoodState foodState)
    {
        if(FoodDatabase == null)
        {
            Debug.Log("Food Database is null", gameObject);
            return true;
        }

        return FoodDatabase.IsContainStateOfFood(food.GetFoodType(),foodState);
    }

    public FoodData GetFoodData(FoodType foodType, FoodState foodState)
    {
        return FoodDatabase.GetFoodData(foodState, foodType);
    }

    public bool CanCombineFood(Food food1,Food food2,out FoodData foodData)
    {
        foodData = null;
        if (food1 == null || food2 == null) return false;
        var list = new List<FoodData> {food1.GetData(), food2.GetData()};
        if (Recipes.IsValid(list))
        {
            foodData = Recipes.FoodResult;
            return true;
        }
        return false;
    }
   
}
