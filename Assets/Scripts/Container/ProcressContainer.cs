using NaughtyAttributes;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IOnDoAction
{
    void DoAction();
}
public class ProcressContainer : HolderAbstract, IOnDoAction
{
    [SerializeField] private FoodState foodStateWantToChange;
    
    [SerializeField] private bool canTimer;
    [SerializeField] private bool isProcessItem;
    
    [SerializeField] private float timer;
    [SerializeField] private float timeToConvert = 1;

    [SerializeField] private UIFoodProcessBar uiProcessBar;

    
    IFoodProcess<Food> foodProcess;
    [SerializeField] private bool infinityConvert;
    
    private void Awake()
    {
        foodProcess = new FoodProcess(foodStateWantToChange);
    }

    
    public void DoAction()
    {
        Debug.Log("Call");
        if (item == null || isProcessItem) return;

        if (foodProcess.CanConvert(GetFood()))
        {
            StartCoroutine(StartConvert(() =>
            {
                foodProcess.Convert(GetFood());
            }));
        }

    }
 
    private IEnumerator StartConvert(Action callback)
    {
        if (uiProcessBar != null || isProcessItem) yield break;
        isProcessItem = true;
        uiProcessBar = UIFoodProcessBarManager.instance.GetUIElement(placeTransform.position);

        while (timer <= timeToConvert)
        {
            if (uiProcessBar != null && timer >= 0 && timer <= timeToConvert)
            {
                uiProcessBar.SetFillAmount(timer / timeToConvert);
            }

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (infinityConvert == false)
        {
            callback?.Invoke();
        }
        timer = 0;
        uiProcessBar?.OnRelease();
        uiProcessBar = null;
        isProcessItem = false;
    }
}
public class FoodProcess : IFoodProcess<Food>
{
    public FoodProcess(FoodState newFoodState)
    {
        foodStateWantToChange = newFoodState;
    }
    public FoodState foodStateWantToChange;
    public void Convert(Food food)
    {
        var foodData = FoodManager.instance.GetFoodData(food.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }
    public bool CanConvert(Food food)
    {
        return food.GetFoodState() != foodStateWantToChange;
    }
}

public class FoodInCookwareProcess : IFoodProcess<Cookware>
{
    public bool CanConvert(Cookware heldItem)
    {
        return true;
    }

    public void Convert(Cookware heldItem)
    {
    }
}
public interface IFoodProcess<T>
{
    bool CanConvert(T heldItem);
    void Convert(T heldItem);
}