using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// [FilePath("SomeSubFolder/StateFile.foo", FilePathAttribute.Location.PreferencesFolder)]
[CreateAssetMenu(fileName = "ContainerBuilderManager",menuName = "Container Builder Manager")]
public class ContainerBuilderManager : ScriptableObject
{
    public List<FoodContainer> foodList;
    [Serializable]
    public struct FoodContainer
    {
        public float spawnScale;
        public Food Prefab;
        public GameObject Model;
        public FoodType foodType;
    }
}