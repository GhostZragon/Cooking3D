using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RecipeOrderProcessor : MonoBehaviour
{
    [Header("List")]
    [SerializeField] private List<RecipeOrder> RecipeOrderListOnShow = new List<RecipeOrder>();
    [SerializeField] private List<RecipeOrder> newRecipeOrderList;
    [Header("References")]
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private Recipes recipes; // current recipe of game level
    [Header("Settings")]
    [SerializeField] private float orderTimeOut = 10f;
    [SerializeField] private int orderCount = 0;
    [SerializeField] private int maxOrderCount = 5;

    // two action use for callback when final animation of recipe is run done,
    // it mean when it start to couning time
    
    private bool requestUpdate = false;
    private void CreateAnimationCallback(RecipeOrder activeRecipeOrder)
    {
        Debug.Log("Pop up callback");
        // remove from list
        RecipeOrderListOnShow.Add(activeRecipeOrder);
    }
    private void DeleteAnimationCallback(RecipeOrder activeRecipeOrder)
    {
        Debug.Log("Pop in callback");
    }

    [Button]
    public void CreateOrder()
    {
        if(orderCount > maxOrderCount)
        {
            return;
        }

        InitDict(recipes);
    }
    public int GetOrderCount() => orderCount;

    private void InitDict(Recipes newRecipe)
    {
        var uiRecipeOrder = ServiceLocator.Current.Get<UIOrderManager>().InitUIOrder();
        if(uiRecipeOrder == null)
        {
            Debug.LogError("UI Order manager pool get UI recipe order null");
            return;
        } 
        var activeRecipeOrder = new RecipeOrder(newRecipe, uiRecipeOrder);
        
        activeRecipeOrder.SetEnableUI(false);
        activeRecipeOrder.SetTimer(orderTimeOut);
        
        uiRecipeOrder.SetCreationAnimationCallback(() => CreateAnimationCallback(activeRecipeOrder));
        uiRecipeOrder.SetDeletionAnimationCallback(() => DeleteAnimationCallback(activeRecipeOrder));
        
        //activeRecipeOrder.recipes = recipeOrderItem;
        
        newRecipeOrderList.Add(activeRecipeOrder);
        requestUpdate = true;

        orderCount++;


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
        orderCount--;
    }

}