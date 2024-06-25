using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    public List<Recipes> recipes = new List<Recipes>();
    
}