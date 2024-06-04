using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct FoodStruct
{
    public PrepareTechniques PrepareTechniques;
    public GameObject Model;
}
[CreateAssetMenu(fileName = "FoodData_1",menuName = "FoodData")]
public class FoodData : ScriptableObject
{
    public FoodType type;
    public List<FoodStruct> FoodStructs;
}