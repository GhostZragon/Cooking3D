using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject RawModel;
    public GameObject DirtyModel;
    public Transform PlaceTransform;
    public List<FoodInPlate> FoodInPlates;
    private void Awake()
    {
        DirtyModel.SetActive(false);
    }

    public void Add(Food food)
    {
        food.transform.parent = PlaceTransform;
        food.transform.localPosition = Vector3.zero;
        
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
