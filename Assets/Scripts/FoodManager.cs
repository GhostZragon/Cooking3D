using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;
    private const string mat_Name = "restaurantbits_mat";

    [SerializeField] private FoodDatabase FoodDatabase;
    [SerializeField] private Food foodPrefab;
    [SerializeField] private Recipes Recipes;
    [SerializeField] private Material food_mat;


    private void Awake()
    {
        instance = this;
    }

    private void OnValidate()
    {
        if (food_mat == null)
        {
            Debug.Log(mat_Name);
            food_mat = Resources.Load<Material>(ConstantPath.Resource.COOKING_MATERIAL);
        }
        if (foodPrefab == null)
        {
            Debug.LogWarning("Food uiItemPrefab is null", gameObject);
        }
    }

    public Food GetFoodInstantiate(FoodType foodType, FoodState foodState)
    {
        return InitFoodState(FoodDatabase.GetFoodData(foodState, foodType));
    }

    private Food InitFoodState(FoodData foodData)
    {
        var food = Instantiate(foodPrefab);
        food.SetData(foodData);
        var skin = food.GetComponent<FoodCustomizeMesh>();
        skin.SetMaterial(food_mat);
        skin.SetMesh(foodData.GetMesh());
        return food;
    }
    public bool CheckFoodValidToChange(Food food, FoodState foodState)
    {
        if (FoodDatabase == null)
        {
            Debug.Log("Food Database is null", gameObject);
            return true;
        }

        return FoodDatabase.CanTransitionToFoodState(food, foodState);
    }

    public FoodData GetFoodData(FoodType foodType, FoodState foodState)
    {
        return FoodDatabase.GetFoodData(foodState, foodType);
    }

  
    public List<Recipes> RecipesList;

    public bool IsFoodInRecipe(FoodData foodData, out List<Recipes> listRecipeValid)
    {
        listRecipeValid = new List<Recipes>();
        foreach (var recipe in RecipesList)
        {
            if (recipe.IsContain(foodData))
            {
                listRecipeValid.Add(recipe);
            }
        }
        return listRecipeValid.Count > 0;
    }

#if UNITY_EDITOR
    [Header("Testing")]
    private FoodState foodState;
    private FoodType foodType;
    [Button]
    private void SupportTesting()
    {
        GetFoodInstantiate(foodType, foodState);
    }
#endif
}
