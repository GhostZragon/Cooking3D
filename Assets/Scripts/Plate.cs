using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : PickUpAbtract
{
    public GameObject RawModel;
    public GameObject DirtyModel;
    public Transform PlaceTransform;
    public List<FoodInPlate> FoodInPlates;
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
        FoodInPlates.Add(foodInPlate);
    }
    public struct FoodInPlate
    {
        public FoodType type;
        public PrepareTechniques PrepareTechniques;
    }

    public bool CanAddFoodToPlate() => true;
}
