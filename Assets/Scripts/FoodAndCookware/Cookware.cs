using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] private List<FoodData> foodDatas;

    [SerializeField] private List<Recipes> recipeMath;
    private void Awake()
    {
        foodDatas = new List<FoodData>();
        recipeMath = new List<Recipes>();
    }

    public Food GetFood()
    {
        return FoodInPlates;
    }
    public List<FoodData> GetContainedFoodData() => foodDatas;
    public void Swap(Food food)
    {
        // if(food.GetCurrentFoodState() == FoodState.Raw) return;
        food?.SetToParentAndPosition(PlaceTransform);
        FoodInPlates = food;
        if (foodDatas.Count == 0)
        {
            foodDatas.Add(food.GetData());
        }
        else
        {
            foodDatas[0] = food.GetData();
        }

    }

    public void DiscardFood()
    {
        foreach (var food in foodDatas)
        {
            Destroy(FoodInPlates.gameObject);
        }
        foodDatas = new List<FoodData>();
    }

    public bool IsContainFoodInPlate()
    {
        return foodDatas.Count > 0;
    }

    public bool CanSwapFood(Food food)
    {
        Debug.LogWarning("IF have process food need multiple item, PLS Upgrade this check");
        if(type == CookwareType.Plate)
        {
            return CanPutFood(food);
        }
        else
        {
            return CookwareManager.instance.CanPutFoodInCookware(type, food);
        }
    }
    public bool CanPutFood(Food food)
    {
        if (foodDatas.Count == 0)
        {
            if (FoodManager.instance.IsFoodInRecipe(food, out recipeMath) == false) return false;
        }

        if (IsFoodInRecipeMatch(food)) return true;
        //if (foodDatas[1] != null) return false;
        Debug.LogWarning("TODO: Need to check food is valid in here");
        return true;
    }
    private bool IsFoodInRecipeMatch(Food food)
    {
        if (recipeMath.Count == 0) return true;
        foreach (var recipe in recipeMath)
        {
            if (recipe.IsContain(food.GetData()))
            {
                return true;
            }
        }

        // remove recipe of it not need
        return false;
    }
    public CookwareType GetCookwareType() => type;
    public void SetCookwareType(CookwareType newType)
    {
        type = newType;
        foreach (var item in listModel)
        {
            if (item.type == type)
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

    public void CombineFood(Food targetFood)
    {
        foodDatas.Add(targetFood.GetData());
        foreach (var recipe in recipeMath)
        {
            var itemCount = recipe.GetFoodCount();
            if (foodDatas.Count >= itemCount) continue;
            int notMatchItem = 0;
            var list = recipe.GetFoodList();
            foreach (var item in foodDatas)
            {
                if (!list.Contains(item))
                {
                    break;
                }
            }
            if (notMatchItem == 0)
            {
                Debug.Log("math some of food");

                if (foodDatas.Count == list.Count)
                {
                    FoodInPlates.SetData(recipe.FoodResult);
                    FoodInPlates.SetModel();
                }
            }
        }
    }
    public void CombineFood(List<FoodData> newFoodDatas)
    {
        foodDatas.AddRange(newFoodDatas);
    }
    //public bool TryToCombineFood(Food newFood)
    //{
    //    if (FoodManager.instance.CanCombineFoodInCookware(newFood, this, out FoodData newFoodDataModel))
    //    {
    //        CombineFood(newFood);
    //        if (newFoodDataModel != null)
    //        {
    //            FoodInPlates.SetData(newFoodDataModel);
    //            FoodInPlates.SetModel();
    //        }
    //        return true;
    //    }
    //    return false;
    //}
    //public bool TryToCombineFood(Cookware cookware2)
    //{
    //    if (FoodManager.instance.CanCombineFoodInCookware(this, cookware2, out FoodData newFoodDataModel))
    //    {
    //        CombineFood(new List<FoodData>(cookware2.GetContainedFoodData()));
    //        if(newFoodDataModel != null)
    //        {
    //            FoodInPlates.SetData(newFoodDataModel);
    //            FoodInPlates.SetModel();
    //        }
    //        return true;
    //    }
    //    return false;
    //}
}