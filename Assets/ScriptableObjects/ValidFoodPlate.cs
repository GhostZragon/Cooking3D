using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ValidFoodPlate", menuName = "ValidFoodCookware/Plate")]
public class ValidFoodPlate : ScriptableObject
{
    public List<Recipes> RecipesList;
    public FoodData foodDataNeedToCheck;
    public List<Recipes> recipesHaveFood;
    [Button]
    public void Test()
    {
        recipesHaveFood.Clear();
        foreach(var recipe in RecipesList)
        {
            if (recipe.IsContain(foodDataNeedToCheck))
            {
                recipesHaveFood.Add(recipe);
            }
        }
    }
}