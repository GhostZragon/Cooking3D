using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodDataManager",menuName = "FoodDataManager")]
public class FoodDataManager : ScriptableObject
{
   public List<FoodData> List;
   public FoodData foodData;
   public bool show;
}