using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "HolderValidDatabase", menuName = "ScriptableObjects/HolderValidDatabase", order = 1)]
public class HolderValidDatabase : ScriptableObject
{
    public List<FoodData> FoodDatas;
    public bool cookwareValidAll;
    public bool foodStateValidAll;
    public bool foodTypeValidAll;
    private void LoadAllFoodData()
    {
    }
    [Button]
    private void LoadAllFoodPrefab()
    {
        string[] guids = AssetDatabase.FindAssets("t:FoodData");
        FoodDatas.Clear();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(path);
            var food = AssetDatabase.LoadAssetAtPath<FoodData>(path);
            FoodDatas.Add(food);
        }
    }
}

