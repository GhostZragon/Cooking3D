using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FoodDataManager))]
public class FoodDataManagerEditor : Editor
{
    private List<FoodData> list;

    public override void OnInspectorGUI()
    {
        FoodDataManager foodDataManager = (FoodDataManager)target;


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Food Data", EditorStyles.boldLabel);
        if (GUILayout.Button("Refresh"))
        {
            foodDataManager.List = LoadAllData(foodDataManager.List);
            list = foodDataManager.List;
        }

        int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));
        foodDataManager.show = EditorGUILayout.Foldout(foodDataManager.show, "Data", true);
        if (foodDataManager.show)
        {
            EditorGUI.indentLevel++;
            while (size > list.Count)
            {
                list.Add(null);
            }

            while (size < list.Count)
            {
                list.RemoveAt(list.Count - 1);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(FoodData), true) as FoodData;
                DrawDetail(list[i]);
            }

            EditorGUI.indentLevel--;
        }
    }

    private void DrawDetail(FoodData foodData)
    {
        if (foodData == null) return;
        // Draw FoodType
        EditorGUI.indentLevel++;
        foodData.type = (FoodType)EditorGUILayout.EnumPopup("Type", foodData.type);
        EditorGUI.indentLevel--;
    }

    private List<FoodData> LoadAllData(List<FoodData> foodsData)
    {
        foodsData.Clear();
        string[] guids = AssetDatabase.FindAssets("t:FoodData", null);
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            FoodData foodData = AssetDatabase.LoadAssetAtPath<FoodData>(path);
            Debug.Log("Path " + path);
            if (foodData != null)
            {
                foodsData.Add(foodData);
            }
        }

        return foodsData;
    }

    private bool HasDuplicatePrepareTechniques(List<FoodStruct> foodStructs, out List<PrepareTechniques> duplicates)
    {
        HashSet<PrepareTechniques> uniqueTechniques = new HashSet<PrepareTechniques>();
        duplicates = new List<PrepareTechniques>();

        foreach (var foodStruct in foodStructs)
        {
            if (!uniqueTechniques.Add(foodStruct.PrepareTechniques))
            {
                duplicates.Add(foodStruct.PrepareTechniques);
            }
        }

        return duplicates.Count > 0;
    }
}