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

    private bool requestUpdate = false;



    [Button]
    private void Testing()
    {
        InitDict(recipes);
    }
    private void InitDict(Recipes newRecipe)
    {
        var uiRecipeOrder = UIOrderManager.instance.InitUIOrder();

        var activeRecipeOrder = new RecipeOrder(newRecipe, uiRecipeOrder);
        activeRecipeOrder.SetEnableUI(false);
        activeRecipeOrder.SetTimer(orderTimeOut);
        uiRecipeOrder.SetPopUpDoneCallBack(() => OnPopUp(activeRecipeOrder));
        uiRecipeOrder.SetUpdateLayoutCallback(() => OnPopIn(activeRecipeOrder));
        //activeRecipeOrder.recipes = recipeOrderItem;
        newRecipeOrderList.Add(activeRecipeOrder);
        requestUpdate = true;
    }
    private void OnPopUp(RecipeOrder activeRecipeOrder)
    {
        Debug.Log("Pop up callback");
        RecipeOrderListOnShow.Add(activeRecipeOrder);
    }
    private void OnPopIn(RecipeOrder activeRecipeOrder)
    {
        Debug.Log("Pop in callback");

        //activeRecipeOrder.TriggerUIOrderRelease();
    }


    private void Update()
    {
        ProcessPendingRecipeOrders();

        ProcessActiveRecipeOrders();
    }

    private void ProcessActiveRecipeOrders()
    {
        if (RecipeOrderListOnShow.Count == 0) return;


        RecipeOrder activeRecipeOrder = null;
        for (int i = 0; i < RecipeOrderListOnShow.Count; i++)
        {
            activeRecipeOrder = RecipeOrderListOnShow[i];
            activeRecipeOrder.Counter(Time.deltaTime);
            if (activeRecipeOrder.HasTimerEnded())
            {
                // Call incorrectly animation here
                RemoveOrderFromList(activeRecipeOrder);
            }
        }
    }

    private void ProcessPendingRecipeOrders()
    {
        if (requestUpdate == false) return;

        if (newRecipeOrderList.Count > 0)
        {
            foreach (var pendingRecipeOrder in newRecipeOrderList)
            {
                // Call popup animaton here
                pendingRecipeOrder.SetEnableUI(true);
                pendingRecipeOrder.TriggerUIOrderShow();
                //RecipeOrderListOnShow.Add(pendingRecipeOrder);

            }
            newRecipeOrderList.Clear();
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
                RemoveOrderFromList(recipeOrder);
                match = true;
            }

        }
        return match;
    }

    private void RemoveOrderFromList(RecipeOrder recipeOrder)
    {
        // Call popin animation here
        Debug.Log("Remove order in here");
        RecipeOrderListOnShow.Remove(recipeOrder);
        recipeOrder.TriggerUIOrderRelease();
    }

}
