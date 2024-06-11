using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodDataManager",menuName = "FoodDataManager")]
public class FoodDataManager : ScriptableObject
{
   [SerializeField]private List<Food> FoodInGamePrefabs = new List<Food>();
   [Expandable]
   [SerializeField] private List<FoodData> FoodDatas;
   [Button]
   private void LoadFoodPrefabInAsset()
   {
      string[] guids = AssetDatabase.FindAssets("l:food");
      FoodInGamePrefabs.Clear();
      foreach (var guid in guids)
      {
         var path = AssetDatabase.GUIDToAssetPath(guid);
         var food = AssetDatabase.LoadAssetAtPath<Food>(path);
         // FoodStates.Add(InitFoodState(food));
         FoodInGamePrefabs.Add(food);
      }
   }
   [Button]
   private void LoadFoodDataInAsset()
   {
      string[] guids = AssetDatabase.FindAssets("t:foodData");
      FoodDatas.Clear();
      foreach (var guid in guids)
      {
         var path = AssetDatabase.GUIDToAssetPath(guid);
         var food = AssetDatabase.LoadAssetAtPath<FoodData>(path);
         // FoodStates.Add(InitFoodState(food));
         FoodDatas.Add(food);
      }
   }
   [Button]
   private void BoundDataFromPrafab()
   {
      foreach (var foodData in FoodDatas)
      {
         foreach (var foodPrefab in FoodInGamePrefabs)
         {
            if (foodData.type == foodPrefab.GetFoodType())
            {
               foodData.InitFoodState(foodPrefab);
               break;
            }
         }
      }
   }
}