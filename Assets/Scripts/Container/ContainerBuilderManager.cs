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
#if UNITY_EDITOR
    [Button]
    private void LoadAllFoodPrefab()
    {
        string[] guids = AssetDatabase.FindAssets("l:food");
        foodList.Clear();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var food = AssetDatabase.LoadAssetAtPath<Food>(path);
            foodList.Add(InitFoodContainer(food));
        }
    }
    private FoodContainer InitFoodContainer(Food prefab)
    {
        var foodContainer = new FoodContainer
        {
            Prefab = prefab,
            spawnScale = 1,
            foodType = prefab.GetFoodType()
        };
        return foodContainer;
    }
#endif
    


    [Serializable]
    public struct FoodContainer
    {
        public float spawnScale;
        public Food Prefab;
        public FoodType foodType;
    }
}