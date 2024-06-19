using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : ScriptableObject
{
    [SerializeField] private GameObject modelObj;
    [SerializeField] private FoodState foodState;
    [SerializeField] private FoodType foodType;

    public GameObject ModelObj
    {
        get => modelObj;
        set => modelObj = value;
    }

    public FoodState FoodState
    {
        get => foodState;
        set => foodState = value;
    }

    public FoodType FoodType
    {
        get => foodType;
        set => foodType = value;
    }
}