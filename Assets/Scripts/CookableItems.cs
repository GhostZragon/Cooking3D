using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "CookableItem",menuName = "Cookable item")]
public class CookableItems : ScriptableObject
{
    public List<FoodState> FoodStates;
    [Serializable]
    public struct FoodState
    {
        public FoodType type;
        public List<RuntimeFoodData> RuntimeFoodData;
    }
    [Button]
    private void Test()
    {
        string[] guids = AssetDatabase.FindAssets("l:food");
        FoodStates.Clear();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var food = AssetDatabase.LoadAssetAtPath<Food>(path);
            FoodStates.Add(InitFoodState(food));
        }
    }

    private FoodState InitFoodState(Food food)
    {
        var foodState = new FoodState()
        {
            type = food.GetFoodType(),
            RuntimeFoodData = food.GetRuntimeFoodData()
        };
        return foodState;
    }
}
