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
    [SerializeField] private FoodData[] foodList;
    
    private int foodIndex = 0;
    
    private void Awake()
    {
        foodList = new FoodData[4];
    
    }
    
    public Food GetFood()
    {
        return FoodInPlates;
    }

    public void Swap(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food?.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;
        foodList[0] = food.GetData();
    }

    public void DiscardFood()
    {
        foreach(var food in foodList)
        {
            Destroy(FoodInPlates.gameObject);
        }
        foodList = new FoodData[4];
        foodIndex = 0;
    }

    public bool IsContainFoodInPlate()
    {
        return foodList.Length > 0;
    }

    public bool CanPutFoodIn(Food food)
    {
        if (foodList[1] != null) return false;
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

    internal void CombineFood(Food targetFood)
    {
        ++foodIndex;
        foodList[foodIndex] = targetFood.GetData();
    }
}