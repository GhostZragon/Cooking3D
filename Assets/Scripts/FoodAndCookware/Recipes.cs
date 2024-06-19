using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe",menuName = "Recipe")]
public class Recipes : ScriptableObject
{
    [SerializeField] private List<FoodData> foodNeed;
}