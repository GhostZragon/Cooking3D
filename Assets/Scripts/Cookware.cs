using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum CookwareType
{
    Plate,
    Pot,
    Pan,
}
public class Cookware : PickUpAbtract
{
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private List<Food> FoodInPlates;
    [SerializeField] private CookwareType type;

    public void Add(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food.SetToParentAndPosition(PlaceTransform);
        FoodInPlates.Add(food);
    }

    public bool IsContainFoodInPlate() => FoodInPlates != null && FoodInPlates.Count > 0;
    public bool CanPutFoodIn(Food food)
    {
        foreach (var _food in FoodInPlates)
        {
            if (_food.GetFoodType() == food.GetFoodType())
            {
                Debug.Log("Same");
                return false;
            }
        }
        // TODO: Need to check food is valid in here
        return true;
    }

    public void DeleteAllFood()
    {
        foreach (var item in FoodInPlates)
        {
            Destroy(item.gameObject);
        }
        FoodInPlates.Clear();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public CookwareType GetCookwareType() => type;
}
