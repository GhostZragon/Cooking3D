using NaughtyAttributes;
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
    [Button]
    private void LoadRecipeInAsset()
    {
        recipes = LoadAssetHeplder.GetListTypeInAssets<Recipes>();
    }
    private void OnEnable()
    {
        LoadRecipeInAsset();
    }
}