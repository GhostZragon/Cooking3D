using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ValidFood",menuName = "Holder Valid Food")]
public class HolderValidFood : ScriptableObject
{
    public List<CookwareType> cookwareType;
    public List<FoodType> FoodTypes;
}
