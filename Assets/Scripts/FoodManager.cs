using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private FoodDatabase FoodDatabase;
    [SerializeField] private Food foodPrefab;
    
    public Food GetFoodInstantiate(FoodType foodType, FoodState foodState)
    {
        var foodData = FoodDatabase.GetFoodData(foodState, foodType);
        if (foodData == null)
        {
            Debug.LogError("This data of food is null");
            return null;
        }

        var food = CreateFood(foodData);
        return food;
    }

    public FoodData GetFoodData(FoodType foodType, FoodState foodState)
    {
        return FoodDatabase.GetFoodData(foodState, foodType);
    }

    private Food CreateFood(FoodData foodData)
    {
        if (foodPrefab == null) return null;
        var food = Instantiate(foodPrefab);
        food.SetData(foodData);
        food.SetModel();
        return food;
    }
}
