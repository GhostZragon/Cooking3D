using System;
using System.Collections.Generic;
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
    [Serializable] 
    public struct CookwareModel
    {
        public CookwareType type;
        public GameObject Model;
    }
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private Food FoodInPlates;
    [SerializeField] private CookwareType type;
    [SerializeField] private List<CookwareModel> listModel;
    [SerializeField] private List<Food> foodList;
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
            return FoodInPlates.GetFoodState() != FoodState.Cooked;
        // TODO: Need to check food is valid in here
        Debug.LogWarning("TODO: Need to check food is valid in here");
        return true;
    }

    public CookwareType GetCookwareType() => type;
    public void SetCookwareType(CookwareType newType)
    {
        type = newType;
        foreach(var item in listModel)
        {
            if(item.type == type)
            {
                item.Model.SetActive(true);
                continue;
            }
            item.Model.SetActive(false);
        }
    }
    public override void Discard()
    {
    }
}