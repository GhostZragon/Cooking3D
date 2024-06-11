using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="FoodData",menuName ="Food Data")]
public class FoodData : ScriptableObject
{
    public FoodType type;
    public List<PrepareTechniques> prapareTech;
    public void InitFoodState(Food food)
    {
        prapareTech.Clear();
        prapareTech = food.GetPrepareTechList();
    }
}