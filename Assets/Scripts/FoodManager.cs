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
            food_mat = Resources.Load<Material>("mat_Name");
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
    public Food GetFoodInstantiate(FoodData foodData)
    {
        return InitFoodState(foodData);
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


    public FoodData GetFoodData(FoodType foodType, FoodState foodState)
    {
        return FoodDatabase.GetFoodData(foodState, foodType);
    }

    public bool CanCombineFood(Food food1,Food food2,out FoodData foodData)
    {
        foodData = null;
        var list = new List<FoodData> {food1.GetData(), food2.GetData()};
        if (Recipes.IsValid(list))
        {
            foodData = Recipes.FoodResult;
            return true;
        }
        return false;
    }
}
