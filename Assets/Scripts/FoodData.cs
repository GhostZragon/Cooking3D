using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
[CreateAssetMenu(fileName ="FoodData",menuName ="Food Data")]
public class FoodData : ScriptableObject
{
    public FoodType type;
    public RuntimeFoodData rawFoodState;
    public List<RuntimeFoodData> TransformFoods;
}