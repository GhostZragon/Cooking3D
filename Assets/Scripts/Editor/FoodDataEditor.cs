using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(FoodData))]
public class FoodDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        FoodData foodData = (FoodData)target;

        // Check for duplicate PrepareTechniques
        if (HasDuplicatePrepareTechniques(foodData.FoodStructs, out List<PrepareTechniques> duplicates))
        {
            // Display warning if duplicates are found
            string duplicateMessage = "Duplicate PrepareTechniques found: " + string.Join(", ", duplicates);
            EditorGUILayout.HelpBox(duplicateMessage, MessageType.Error);
        }
        else
        {
            // Display success message if no duplicates
            EditorGUILayout.HelpBox("No duplicate PrepareTechniques found.", MessageType.Info);
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