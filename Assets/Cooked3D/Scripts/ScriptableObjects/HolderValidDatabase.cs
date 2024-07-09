using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "HolderValidDatabase", menuName = "ScriptableObjects/HolderValidDatabase", order = 1)]
public class HolderValidDatabase : ScriptableObject
{
    public List<FoodData> FoodDatas;
    public bool cookwareValidAll;
    public bool foodStateValidAll;
    public bool foodTypeValidAll;

}
