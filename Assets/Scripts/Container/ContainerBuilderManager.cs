using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEditor;
using UnityEngine;
// [FilePath("SomeSubFolder/StateFile.foo", FilePathAttribute.Location.PreferencesFolder)]
[CreateAssetMenu(fileName = "ContainerBuilderManager",menuName = "Container Builder Manager")]
public class ContainerBuilderManager : ScriptableObject
{
    public List<FoodContainer> foodList;
    [Button]
    private void LoadAllFoodPrefab()
    {
        string[] guids = AssetDatabase.FindAssets("l:food");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var food = AssetDatabase.LoadAssetAtPath<Food>(path);
            Debug.Log(food.name);
            Debug.Log("Path " + path);
            // if (foodData != null)
            // {
            //     foodsData.Add(foodData);
            // }
        }
    }
    [Serializable]
    public struct FoodContainer
    {
        public float spawnScale;
        public Food Prefab;
        public GameObject Model;
        public FoodType foodType;
    }
}