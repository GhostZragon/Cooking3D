using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodDatabase", menuName = "FoodDatabase")]
public class FoodDatabase : ScriptableObject
{
    [SerializeField] private FoodState FoodState;
    [SerializeField] private FoodType FoodType;
    [SerializeField] private string path = "Assets/ScriptableObjects/FoodData";
    // [ContextMenu("Create Item")]
    [Button]
    private void CreateFoodData()
    {


        #if UNITY_EDITOR
        var name = FoodState.ToString() + "_" + FoodType.ToString();
        var fullPath = path + $"/{FoodState.ToString()}/" + name + ".asset";
        Debug.Log(fullPath);
        if (File.Exists(fullPath))
        {
            Debug.LogWarning("Duplicate asset");
            return;
        }
        var mySo = ScriptableObject.CreateInstance<FoodData>();
        mySo.FoodState = FoodState;
        mySo.FoodType = FoodType;

        mySo.name = name;

        UnityEditor.AssetDatabase.CreateAsset(mySo, fullPath);
        UnityEditor.AssetDatabase.SaveAssets();
        AddToList(FoodState, mySo);

        #endif
    }

    private void AddToList(FoodState foodState, FoodData foodData)
    {
        if(foodDictData.TryGetValue(foodState,out var list))
        {
            list.Add(foodData);
        }
    }

    [SerializeField, Expandable] List<FoodData> RawFood;
    [SerializeField, Expandable] List<FoodData> SliceFood;
    [SerializeField, Expandable] List<FoodData> CookedFood;
    [SerializeField, Expandable] List<FoodData> MixxedFood;
    private Dictionary<FoodState, List<FoodData>> foodDictData;

    private void OnEnable()
    {
        if (foodDictData == null)
        {
            foodDictData = new Dictionary<FoodState, List<FoodData>>
            {
                { FoodState.Raw, RawFood },
                { FoodState.Slice, SliceFood },
                { FoodState.Cooked, CookedFood },
                {FoodState.Mixed,MixxedFood }

            };
        }
    }
    public FoodData GetFoodData(FoodState foodState, FoodType foodType)
    {
        if (!foodDictData.TryGetValue(foodState, out var list)) return null;
        foreach (var item in list)
        {
            if (item.FoodType == foodType)
                return item;
        }
        Debug.LogError($"Food not in database !!!{foodState.ToString()} + {foodType.ToString()}", this);
        return null;
    }
    public bool CanTransitionToFoodState(Food food, FoodState foodStateWantToChange)
    {
        // check foodState is contain
        if (!foodDictData.TryGetValue(foodStateWantToChange, out var list)) return false;
        foreach (var _foodData in list)
        {
            // make sure food have same food type in list with key is "Food State"
            if (_foodData.FoodType == food.GetFoodType() && _foodData.CanFoodChangeState(food))
            {
                return true;
            }
        }
        return false;
    }
}