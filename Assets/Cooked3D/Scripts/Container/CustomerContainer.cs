using System.Collections.Generic;
using UnityEngine;

public class CustomerContainer : Container
{
    [SerializeField] private bool allowNotContainFood = false;
    private RecipeOrderProcessor orderProcessor;
    private void Awake()
    {
        orderProcessor = ServiceLocator.Current.Get<RecipeOrderProcessor>();
    }

    private void SpawnText(string text,Color color)
    {
        UITextPopupHandle.ShowTextAction?.Invoke(placeTransform.position, text, color);
    }

    public override void ExchangeItems(HolderAbstract player)
    {
        if (!CanDeliverFood(player))
        {
            SpawnText("I don't want this food!",Color.red);
            return;
        }
        base.ExchangeItems(player);
        //SpawnText("Thank you!",Color.green);
        Debug.Log("On Swap");
    }

    /// <summary>
    /// Checking can give food to player 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private bool CanDeliverFood(HolderAbstract player)
    {
        if (player.IsContainFoodInCookware() == false) return false; // if hold food then return false
        if (player.GetCookwareType() != CookwareType.Plate) return false; // if not hold plate, return false
        //var food = player.GetCookware().GetFood();
        var cookware = player.GetCookware();
        var recipeList = cookware.GetComponent<CookwareRecipeHandle>().GetRecipeList();
        if(recipeList.Count > 0)
        {
            if (orderProcessor.Check(recipeList[0].Recipes,out var scoreGrade))
            {
                ScoreManager.AddScore(scoreGrade,placeTransform.position);

                player.DiscardCookware();
                return true;
            }
        }
        
        return false;
    }
}