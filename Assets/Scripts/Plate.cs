using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plate : PickUpAbtract
{
    [SerializeField] private GameObject RawModel;
    [SerializeField] private GameObject DirtyModel;
    [SerializeField] private Transform PlaceTransform;
    [SerializeField] private List<FoodInPlate> FoodInPlates;
    private void Awake()
    {
        DirtyModel.SetActive(false);
        FoodInPlates = new List<FoodInPlate>();
    }

    public void Add(Food food)
    {
        food.SetToParentAndPosition(PlaceTransform);
        var foodInPlate = new FoodInPlate();
        foodInPlate.type = food.type;
        foodInPlate.PrepareTechniques = food.GetPrepareTech();
        foodInPlate.foodGameObject = food.gameObject;
        FoodInPlates.Add(foodInPlate);
    }
    public struct FoodInPlate
    {
        public FoodType type;
        public PrepareTechniques PrepareTechniques;
        public GameObject foodGameObject;
    }

    public bool IsContainFoodInPlate() => FoodInPlates != null && FoodInPlates.Count > 0;
    public bool CanPutFoodIn(Food food)
    {
        // TODO: Need to check food is valid in here
        return true;
    }

    public void DeleteAllFood()
    {
        foreach (var item in FoodInPlates)
        {
            Destroy(item.foodGameObject);
        }
        FoodInPlates.Clear();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
