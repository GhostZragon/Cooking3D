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
    public static class Ultis
    {
        public static void SwapFoodTwoWay(Cookware cookware1, Cookware cookware2)
        {
            var food1 = cookware1.GetFood();
            var food2 = cookware2.GetFood();
            cookware1.Add(food2);
            cookware2.Add(food1);
        }

    }
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;
    public Food GetFood() => FoodInPlates;

    public void Add(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food?.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;
    }
    public void DiscardFood()
    {
        Destroy(FoodInPlates.gameObject);
        FoodInPlates = null;
    }
    public bool IsContainFoodInPlate() => FoodInPlates != null;
    public bool CanPutFoodIn(Food food)
    {
        if (FoodInPlates != null)
            return FoodInPlates.GetCurrentFoodState() != FoodState.Cooked;
        // TODO: Need to check food is valid in here
        Debug.LogWarning("TODO: Need to check food is valid in here");
        return true;
    }

    public CookwareType GetCookwareType() => type;
    public override void Discard()
    {
    }
}
