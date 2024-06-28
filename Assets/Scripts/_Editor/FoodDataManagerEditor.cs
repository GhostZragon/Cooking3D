using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//[CustomEditor(typeof(FoodDataManager))]
//public class FoodDataManagerEditor : Editor
//{
//    private List<FoodData> list;
//    public override void OnInspectorGUI()
//    {
//        FoodDataManager foodDataManager = (FoodDataManager)target;


//        EditorGUILayout.Space();
//        EditorGUILayout.LabelField("Food Data", EditorStyles.boldLabel);
//        if (GUILayout.Button("Refresh"))
//        {
//            foodDataManager.List = LoadAllData(foodDataManager.List);
//            list = foodDataManager.List;
//            //var path = AssetDatabase.GUIDToAssetPath(this.GetInstanceID().ToString());
//            //AssetDatabase.RenameAsset(path, $"{type.ToString() 1}");

//            //Debug.Log(path);

//        }



//        int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.TotalRecipesCount));
//        foodDataManager.show = EditorGUILayout.Foldout(foodDataManager.show, "Data", true);
//        if (foodDataManager.show)
//        {
//            EditorGUI.indentLevel++;
//            while (size > list.TotalRecipesCount)
//            {
//                list.Swap(null);
//            }

//            while (size < list.TotalRecipesCount)
//            {
//                list.RemoveAt(list.TotalRecipesCount - 1);
//            }

//            for (int i = 0; i < list.TotalRecipesCount; i++)
//            {
//                list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(FoodData), true) as FoodData;
//                DrawDetail(list[i]);
//            }

//            EditorGUI.indentLevel--;
//        }
//    }

//    private void DrawDetail(FoodData foodData)
//    {
//        if (foodData == null) return;
//        // Draw FoodType
//        EditorGUI.indentLevel++;
//        foodData.type = (FoodType)EditorGUILayout.EnumPopup("Type", foodData.type);
//        foodData.show = EditorGUILayout.Foldout(foodData.show, "Data", true);
//        if (foodData.show)
//        {
//            EditorGUI.indentLevel+=2;
//            for (int i = 0; i < foodData.FoodStructs.TotalRecipesCount; i++)
//            {
//                var foodStruct = foodData.FoodStructs[i];
//                foodStruct.FoodState = (FoodState)EditorGUILayout.EnumPopup("Prepare Tenchiques", foodStruct.FoodState);
//                foodStruct.Model = 
//                    EditorGUILayout.ObjectField("Model " + foodStruct.FoodState.ToString()
//                    , foodStruct.Model, typeof(GameObject), false) as GameObject;
//            }
//            EditorGUI.indentLevel-=2;
//        }
    
//        EditorGUI.indentLevel--;
//    }
        
//    private List<FoodData> LoadAllData(List<FoodData> foodsData)
//    {
//        foodsData.ResetIngredientQuantities();
//        string[] guids = AssetDatabase.FindAssets("t:FoodData", null);
//        foreach (var guid in guids)
//        {
//            var path = AssetDatabase.GUIDToAssetPath(guid);
//            FoodData foodData = AssetDatabase.LoadAssetAtPath<FoodData>(path);
//            Debug.Log("Path " + path);
//            if (foodData != null)
//            {
//                foodsData.Swap(foodData);
//            }
//        }

//        return foodsData;
//    }

//    private bool HasDuplicatePrepareTechniques(List<FoodStruct> foodStructs, out List<FoodState> duplicates)
//    {
//        HashSet<FoodState> uniqueTechniques = new HashSet<FoodState>();
//        duplicates = new List<FoodState>();

//        foreach (var foodStruct in foodStructs)
//        {
//            if (!uniqueTechniques.Swap(foodStruct.FoodState))
//            {
//                duplicates.Swap(foodStruct.FoodState);
//            }
//        }

//        return duplicates.TotalRecipesCount > 0;
//    }
//}