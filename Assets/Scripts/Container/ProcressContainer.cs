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
    [SerializeField] private CookwareType cookwareCanPut;
    [SerializeField] private bool canTimer;
    [SerializeField] private bool isProcessItem;
    [SerializeField] private float timer;
    [SerializeField] private float timeToConvert = 1;

    [SerializeField] private UIFoodProcessBar uiProcessBar;
    
    [SerializeField] private bool infinityConvert;
    IFoodProcess<Food> foodProcess;
    ITriggerProcress ITriggerProcess;

    
    private void Awake()
    {
        foodProcess = new FoodProcess(foodStateWantToChange);
        ITriggerProcess = GetComponent<ITriggerProcress>();
    }

    
    public void DoAction()
    {
        Debug.Log("Call");
        if (item == null || isProcessItem) return;

        if (foodProcess.IsConvertible(GetFood()))
        {
            StartCoroutine(StartConvert(() =>
            {
                foodProcess.ChangeFoodState(GetFood());
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
            if (CanProcess() == false)
                break;

            if (uiProcessBar != null && timer >= 0 && timer <= timeToConvert)
            {
                uiProcessBar.SetFillAmount(timer / timeToConvert);
            }

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        
        if (Application.isEditor)
        {
            if (infinityConvert == false)
            {
                callback?.Invoke();
            }
        }
        else
        {
            callback?.Invoke();
        }
        timer = 0;
        uiProcessBar?.OnRelease();
        uiProcessBar = null;
        isProcessItem = false;
    }

    private bool CanProcess()
    {
        if (ITriggerProcess == null) return true;
        return ITriggerProcess.OnTrigger;
    }
}
public class FoodProcess : IFoodProcess<Food>
{
    public FoodProcess(FoodState newFoodState)
    {
        foodStateWantToChange = newFoodState;
    }
    public FoodState foodStateWantToChange;
    public void ChangeFoodState(Food food)
    {
        var foodData = FoodManager.instance.GetFoodData(food.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }
    public bool IsConvertible(Food food)
    {
        bool foodNotSameState = food.GetFoodState() != foodStateWantToChange;
        bool isFoodStateInDatabase = FoodManager.instance.CheckFoodValidToChange(food, foodStateWantToChange);
        return foodNotSameState && isFoodStateInDatabase;
    }
}

public class FoodInCookwareProcess : IFoodProcess<Cookware>
{
    public bool IsConvertible(Cookware heldItem)
    {
        return true;
    }

    public void ChangeFoodState(Cookware heldItem)
    {

    }
}
public interface IFoodProcess<T>
{
    bool IsConvertible(T heldItem);
    void ChangeFoodState(T heldItem);
}