using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RecipeOrderHandle : MonoBehaviour
{
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private List<RecipeOrder> RecipeOrderListOnShow = new List<RecipeOrder>();
    [SerializeField] private List<RecipeOrder> newRecipeOrderList;
    [SerializeField] private Recipes recipes;
    [SerializeField] private float orderTimeOut = 10f;
    [SerializeField] private float refreshRate = .1f;
    private YieldInstruction delayInstruction;
    private void Awake()
    {
        StartCoroutine(StartCounterInfinity());
    }


    [Button]
    private void Testing()
    {
        InitDict(recipes);
    }
    private void InitDict(Recipes newRecipe)
    {
        var uiRecipeOrder = UIOrderManager.instance.InitUIOrder();
    
        var recipeOrder = new RecipeOrder(newRecipe, uiRecipeOrder);
        recipeOrder.SetEnableUI(false);
        recipeOrder.SetTimer(orderTimeOut);
        uiRecipeOrder.SetPopUpCallBack(() => OnPopUp(recipeOrder));
        uiRecipeOrder.SetPopInCallback(() => OnPopIn(recipeOrder));
        //recipeOrder.recipes = recipeOrderItem;
        newRecipeOrderList.Add(recipeOrder);
    }
    private void OnPopUp(RecipeOrder recipeOrder)
    {
        Debug.Log("On pop up UI");
        //RecipeOrderListOnShow.Add(recipeOrder);
    }
    private void OnPopIn(RecipeOrder recipeOrder)
    {
        Debug.Log("On pop in UI");
        //recipeOrder.RemoveUI();
    }
    private IEnumerator StartCounterInfinity()
    {
        delayInstruction = new WaitForSeconds(refreshRate);
        RecipeOrder recipeOrder = null;

        while (true)
        {
            if (newRecipeOrderList.Count > 0)
            {
                foreach (var item in newRecipeOrderList)
                {
                    // Call popup animaton here
                    item.SetEnableUI(true);
                    RecipeOrderListOnShow.Add(recipeOrder);
                }
                newRecipeOrderList.Clear();
            }

            for (int i = 0; i < RecipeOrderListOnShow.Count; i++)
            {
                recipeOrder = RecipeOrderListOnShow[i];
                recipeOrder.Counter(refreshRate);
                if (recipeOrder.IsTimeOut())
                {
                    // Call incorrectly animation here
                    recipeOrder.RemoveUI();
                    RemoveOrder(recipeOrder);
                }
            }
            yield return delayInstruction;
        }
    }

    public bool Check(Recipes recipes)
    {
        bool match = false;
        RecipeOrder recipeOrder = null;
        for (int i = 0; i < RecipeOrderListOnShow.Count; i++)
        {
            recipeOrder = RecipeOrderListOnShow[i];
            if (recipeOrder.IsMatchRecipe(recipes))
            {
                // Correct animation
                RemoveOrder(recipeOrder);
                match = true;
            }

        }
        return match;
    }

    private void RemoveOrder(RecipeOrder recipeOrder)
    {
        // Call popin animation here
        recipeOrder.RemoveUI();
        RecipeOrderListOnShow.Remove(recipeOrder);
    }
}
