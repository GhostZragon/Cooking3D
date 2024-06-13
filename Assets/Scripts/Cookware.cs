using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum CookwareType
{
    None,
    Plate,
    Pot,
    Pan,
}
public class Cookware : PickUpAbtract
{
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;

    public void Add(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;
    }

    public bool IsContainFoodInPlate() => FoodInPlates != null;
    public bool CanPutFoodIn(Food food)
    {
        if (this.FoodInPlates != null)
            return food.GetCurrentFoodState() != FoodState.Cooked;
        // TODO: Need to check food is valid in here
        Debug.LogWarning("TODO: Need to check food is valid in here");
        return true;
    }
    public Food GetFood() => FoodInPlates;
    public bool IsEqualCookwareType(CookwareType type)
    {
        return this.type == type;
    }
    public void DeleteAllFood()
    {
        Destroy(FoodInPlates.gameObject);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public CookwareType GetCookwareType() => type;
}
