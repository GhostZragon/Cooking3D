using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "FoodDatabase", menuName = "FoodDatabase")]
public class FoodDatabase : ScriptableObject
{
    public FoodState FoodState;
    public FoodType FoodType;
    public GameObject modelObj;
    public string path = "Assets/ScriptableObjects/FoodData";

    // [ContextMenu("Create Item")]
    [Button]
    private void CreateFoodData()
    {
        if (modelObj == null)
        {
            Debug.LogError("Model is null");
            return;
        }

        var mySo = ScriptableObject.CreateInstance<FoodData>();
        mySo.FoodState = FoodState;
        mySo.FoodType = FoodType;
        mySo.ModelObj = modelObj;

// #if UNITY_EDITOR
        var name = FoodState.ToString() + "_" + FoodType.ToString();
        var fullPath = path +$"/{FoodState.ToString()}/"+ name + ".asset";
        Debug.Log(fullPath);
        if (File.Exists(fullPath))
        {
            Debug.LogWarning("Duplicate asset");
            return;
        }
        mySo.name = name;
        
        UnityEditor.AssetDatabase.CreateAsset(mySo, fullPath);
        UnityEditor.AssetDatabase.SaveAssets();
        AddToList(FoodState,mySo);
// #endif
    }

    private void AddToList(FoodState foodState, FoodData foodData)
    {
        switch (foodState)
        {
            case FoodState.Raw:
                RawFood.Add(foodData);
                break;
            case FoodState.Slice:
                SliceFood.Add(foodData);
                break;
            case FoodState.Cooked:
                CookedFood.Add(foodData);
                break;
        }
    }

    public List<FoodData> RawFood;
    public List<FoodData> SliceFood;
    public List<FoodData> CookedFood;
    
}