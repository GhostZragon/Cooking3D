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
    [SerializeField] private GameObject foodPrefab;

    public Food GetFood(FoodType foodType, FoodState foodState)
    {
        var foodData = FoodDatabase.GetData(foodState, foodType);
        if (foodData == null)
        {
            Debug.LogError("This data of food is null");
            return null;
        }

        var food = CreateFood(foodData);
        return food;
    }

    private Food CreateFood(FoodData foodData)
    {
        if (foodPrefab == null) return null;
        var food = Instantiate(foodPrefab).GetComponent<Food>();
        var model = Instantiate(foodData.ModelObj);
        model.transform.SetParent(food.transform);
        model.transform.localPosition = Vector3.zero;

        var mesh = model.AddComponent<MeshCollider>();
        mesh.convex = true;
        mesh.gameObject.layer = LayerMask.NameToLayer("Food");
        
        food.SetData(foodData);
        food.Init();
        return food;
    }
}
