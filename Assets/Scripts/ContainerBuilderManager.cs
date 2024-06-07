using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[FilePath("SomeSubFolder/StateFile.foo", FilePathAttribute.Location.PreferencesFolder)]
public class ContainerBuilderManager : ScriptableSingleton<ContainerBuilderManager>
{
    public struct FoodContainer
    {
        public GameObject Model;
        public FoodType foodType;
    }
}