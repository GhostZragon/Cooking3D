using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ValidFood",menuName = "Holder Valid Food")]
public class HolderValidFood : ScriptableObject
{
    private List<Food> Foods;
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
            Foods.Add(new FoodValid(food));
        }
    }
#endif
}
