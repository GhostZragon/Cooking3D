using System;
using System.Collections;
using System.Collections.Generic;
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

    public bool HasFoodInPlate() => FoodInPlates != null && FoodInPlates.Count > 0;
    public void Add(Food food)
    {
        food.SetToParentAndPosition(PlaceTransform);
        var foodInPlate = new FoodInPlate();
        foodInPlate.type = food.type;
        foodInPlate.PrepareTechniques = food.GetPrepareTech();
        FoodInPlates.Add(foodInPlate);
    }
    public struct FoodInPlate
    {
        public FoodType type;
        public PrepareTechniques PrepareTechniques;
    }

    public bool CanAddFoodToPlate() => true;
}
