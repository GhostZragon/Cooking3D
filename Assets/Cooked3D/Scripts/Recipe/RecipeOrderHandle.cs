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
        //var uiRecipeOrder = UIOrderManager.instance.InitUIOrder();
    
        var recipeOrder = new RecipeOrder(newRecipe, UIOrderManager.instance.InitUIOrder());
        recipeOrder.SetEnableUI(false);
        recipeOrder.SetTimer(orderTimeOut);
        //uiRecipeOrder.SetPopUpCallBack(() => OnPopUp(activeRecipeOrder));
        //uiRecipeOrder.SetPopInCallback(() => OnPopIn(activeRecipeOrder));
        //activeRecipeOrder.recipes = recipeOrderItem;
        newRecipeOrderList.Add(recipeOrder);
    }
    private void OnPopUp(RecipeOrder recipeOrder)
    {
        Debug.Log("On pop up UI");
        //RecipeOrderListOnShow.Add(activeRecipeOrder);
    }
    private void OnPopIn(RecipeOrder recipeOrder)
    {
        Debug.Log("On pop in UI");
        //activeRecipeOrder.TriggerUIOrderRelease();
    }
    private IEnumerator StartCounterInfinity()
    {
        delayInstruction = new WaitForSeconds(refreshRate);
        RecipeOrder activeRecipeOrder = null;

        while (true)
        {
            if (newRecipeOrderList.Count > 0)
            {
                foreach (var pendingRecipeOrder in newRecipeOrderList)
                {
                    // Call popup animaton here
                    pendingRecipeOrder.SetEnableUI(true);
                    RecipeOrderListOnShow.Add(pendingRecipeOrder);
                }
                newRecipeOrderList.Clear();
            }

            for (int i = 0; i < RecipeOrderListOnShow.Count; i++)
            {
                if(activeRecipeOrder == null)
                {
                    Debug.Log("This is null");
                }
                activeRecipeOrder = RecipeOrderListOnShow[i];
                activeRecipeOrder.Counter(refreshRate);
                if (activeRecipeOrder.HasTimerEnded())
                {
                    // Call incorrectly animation here
                    RemoveOrder(activeRecipeOrder);
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
            if (recipeOrder.IsMatchingRecipe(recipes))
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
        Debug.Log("Remove order in here");
        RecipeOrderListOnShow.Remove(recipeOrder);
        recipeOrder.TriggerUIOrderRelease();
    }
}
