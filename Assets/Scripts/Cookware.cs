using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cookware : PickUpAbtract
{


    [SerializeField] private GameObject RawModel;
    [SerializeField] private GameObject DirtyModel;
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private List<Food> FoodInPlates;
    private void Awake()
    {
        DirtyModel.SetActive(false);
        FoodInPlates = new List<Food>();
    }

    public void Add(Food food)
    {
        food.SetToParentAndPosition(PlaceTransform);
        // var foodInPlate = new FoodInPlate();
        // foodInPlate.type = food.GetFoodType();
        // foodInPlate.PrepareTechniques = food.GetPrepareTech();
        // foodInPlate.foodGameObject = food.gameObject;
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
}
