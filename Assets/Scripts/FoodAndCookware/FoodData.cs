using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
public class FoodData : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private FoodState foodState;
    [SerializeField] private FoodType foodType;

  
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

    public Mesh GetMesh()
    {
        return mesh;
    }
    [Button]
    private void Test()
    {
        Debug.Log(GetMesh().name);
    }
}