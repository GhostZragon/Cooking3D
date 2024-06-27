using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



//[CustomEditor(typeof(FoodData))]
//public class FoodDataEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        // Draw the default inspector
//        DrawDefaultInspector();

//        FoodData foodData = (FoodData)target;

//        // Check for duplicate FoodState
//        if (HasDuplicatePrepareTechniques(foodData.FoodStructs, out List<FoodState> duplicates))
//        {
//            // Display warning if duplicates are found
//            string duplicateMessage = "Duplicate FoodState found: " + string.Join(", ", duplicates);
//            EditorGUILayout.HelpBox(duplicateMessage, MessageType.Error);
//        }
//        else
//        {
//            // Display success message if no duplicates
//            EditorGUILayout.HelpBox("No duplicate FoodState found.", MessageType.Info);
//        }
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