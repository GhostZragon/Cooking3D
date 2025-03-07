using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "ScriptableObjects/RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    [Expandable]
    [SerializeField] private List<Recipes> recipes = new List<Recipes>();
    public List<Recipes> Recipes { get => recipes; }

    public Recipes GetRandomRecipe()
    {
        if (recipes.Count == 0) return null;
        var index = UnityEngine.Random.Range(0, recipes.Count);
        return recipes[index];
    }
#if UNITY_EDITOR
    [Button]
    private void LoadRecipeInAsset()
    {
        recipes = LoadAssetHeplder.GetListTypeInAssets<Recipes>();
    }
    private void OnEnable()
    {
        LoadRecipeInAsset();
    }

#endif
}