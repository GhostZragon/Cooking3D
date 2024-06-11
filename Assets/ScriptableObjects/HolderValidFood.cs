using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
[CreateAssetMenu(fileName = "ValidFood",menuName = "Holder Valid Food")]
public class HolderValidFood : ScriptableObject
{
    [Serializable]
    public struct Test
    {
        public Test(Food food)
        {
            type = food.GetFoodType();
            PrepareTechniquesList = food.GetPrepareTechList();
        }
        public FoodType type;
        public List<PrepareTechniques> PrepareTechniquesList;
    }
    public List<Test> Foods;
#if UNITY_EDITOR
    [Button]
    private void LoadFoodPrefabInAsset()
    {
        string[] guids = AssetDatabase.FindAssets("l:food");
        Foods.Clear();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var food = AssetDatabase.LoadAssetAtPath<Food>(path);
            // FoodStates.Add(InitFoodState(food));
            Foods.Add(new Test(food));
        }
    }
#endif
}
