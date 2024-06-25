using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodDatabase", menuName = "FoodDatabase")]
public class FoodDatabase : ScriptableObject
{
    public FoodState FoodState;
    public FoodType FoodType;
    public string path = "Assets/ScriptableObjects/FoodData";
    // [ContextMenu("Create Item")]
    [Button]
    private void CreateFoodData()
    {

        var mySo = ScriptableObject.CreateInstance<FoodData>();
        mySo.FoodState = FoodState;
        mySo.FoodType = FoodType;

// #if UNITY_EDITOR
        var name = FoodState.ToString() + "_" + FoodType.ToString();
        var fullPath = path + $"/{FoodState.ToString()}/" + name + ".asset";
        Debug.Log(fullPath);
        if (File.Exists(fullPath))
        {
            Debug.LogWarning("Duplicate asset");
            return;
        }

        mySo.name = name;

        UnityEditor.AssetDatabase.CreateAsset(mySo, fullPath);
        UnityEditor.AssetDatabase.SaveAssets();
        AddToList(FoodState, mySo);

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

    [SerializeField,Expandable] List<FoodData> RawFood;
    [SerializeField, Expandable] List<FoodData> SliceFood;
    [SerializeField, Expandable] List<FoodData> CookedFood;
    private Dictionary<FoodState, List<FoodData>> foodDictData;

    private void OnEnable()
    {
        if (foodDictData == null)
        {
            foodDictData = new Dictionary<FoodState, List<FoodData>>
            {
                { FoodState.Raw, RawFood },
                { FoodState.Slice, SliceFood },
                { FoodState.Cooked, CookedFood }
            };
        }
    }
    [SerializeField] private FoodData defaultFoodData;
    public FoodData GetFoodData(FoodState foodState, FoodType foodType)
    {
        if (!foodDictData.TryGetValue(foodState, out var list)) return null;
        foreach (var item in list)
        {
            if (item.FoodType == foodType)
                return item;
        }
        Debug.LogError("Food not in database !!!",this);
        return defaultFoodData;
    }
}