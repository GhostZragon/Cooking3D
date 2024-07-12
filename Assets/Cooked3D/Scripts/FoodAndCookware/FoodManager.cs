using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : ServiceInstaller<FoodManager>, ServiceLocator.IGameService
{
    //public static FoodManager instance;
    private const string mat_Name = "restaurantbits_mat";

    [SerializeField] private FoodDatabase FoodDatabase;
    [SerializeField] private Food foodPrefab;
    [SerializeField] private Recipes Recipes;
    [SerializeField] private Material food_mat;
    [SerializeField] private RecipeDatabase recipeDatabase;
    
    private UnityPool<Food> foodPoolObject;

    protected override void CustomAwake()
    {
        base.CustomAwake();
        foodPoolObject = new UnityPool<Food>(foodPrefab, 5, transform);

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

        var food = foodPoolObject.Get();
        var foodData = FoodDatabase.GetFoodData(foodState, foodType);
        
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
    /// <summary>
    /// Checking is food data is inside some recipe then return list of recipe
    /// </summary>
    /// <param name="foodData"></param>
    /// <param name="listRecipeValid"></param>
    /// <returns></returns>
    public bool IsFoodInRecipe(FoodData foodData, out List<Recipes> listRecipeValid)
    {
        listRecipeValid = new List<Recipes>();
        if(recipeDatabase == null)
        {
            Debug.Log("Recipe database is null");
            return false;
        }
        foreach (var recipe in recipeDatabase.Recipes)
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
