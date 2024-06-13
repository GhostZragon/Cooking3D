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
        public static void SwapFood(Cookware cookware1, Cookware cookware2)
        {
            var food1 = cookware1.GetFood();
            var food2 = cookware2.GetFood();
            //bool canExchangeFoodBetweenCookware = cookware1.CanPutFoodIn(cookware2.GetFood()) &&
            //                                  cookware2.CanPutFoodIn(cookware1.GetFood());
            //if (canExchangeFoodBetweenCookware)
            //{
                
            //}
            cookware1.Add(food2);
            cookware2.Add(food1);
        }
    }
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;

    public void Add(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food?.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;
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
