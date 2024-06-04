using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
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
    public bool show;


}