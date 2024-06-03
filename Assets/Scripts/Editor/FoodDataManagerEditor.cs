using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FoodDataManagerEditor : EditorWindow
{
    private List<FoodData> foodsData = new List<FoodData>();
    private Vector2 scrollPos;

    [MenuItem("ToolsTip/Food Data Manager")]
    static void ShowWindow()
    {
        GetWindow<FoodDataManagerEditor>("Food Data Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Food Data Manager",EditorStyles.boldLabel);

        if (GUILayout.Button("Load All FoodData"))
        {
            LoadAllData();
        }
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (var foodData in foodsData)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(foodData.name, EditorStyles.boldLabel);
            for (int i = 0; i < foodData.FoodStructs.Count; i++)
            {
                var foodStruct = foodData.FoodStructs[i];
                EditorGUILayout.BeginHorizontal();
                foodStruct.PrepareTechniques = (PrepareTechniques)EditorGUILayout.EnumPopup("Prepare Technique", foodStruct.PrepareTechniques);
                foodStruct.Model = (GameObject)EditorGUILayout.ObjectField("Model", foodStruct.Model, typeof(GameObject), false);
                EditorGUILayout.EndHorizontal();
                foodData.FoodStructs[i] = foodStruct;
            }
            if (HasDuplicatePrepareTechniques(foodData.FoodStructs, out List<PrepareTechniques> duplicates))
            {
                string duplicateMessage = "Duplicate PrepareTechniques found: " + string.Join(", ", duplicates);
                EditorGUILayout.HelpBox(duplicateMessage, MessageType.Error);
            }
            else
            {
                EditorGUILayout.HelpBox("No duplicate PrepareTechniques found.", MessageType.Info);
            }
            EditorGUILayout.EndVertical();
        }
        GUILayout.EndScrollView();
        if (GUI.changed)
        {
            foreach (var foodData in foodsData)
            {
                EditorUtility.SetDirty(foodData);
            }
        }
    }

        private void LoadAllData()
        {
            foodsData.Clear();
            string[] guids = AssetDatabase.FindAssets("t:FoodData", null);
            string path = null;
            foreach (var guid in guids)
            {
                path = AssetDatabase.GUIDToAssetPath(guid);
                FoodData foodData = AssetDatabase.LoadAssetAtPath<FoodData>(path);
                Debug.Log("Path " + path);
                if (foodData != null)
                {
                    foodsData.Add(foodData);
                }
            }
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