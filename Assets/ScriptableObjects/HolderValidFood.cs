using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public abstract class HolderValidFood : ScriptableObject
{
    public abstract bool CheckingFoodIsValid(FoodData foodData);
}