using System;
using System.Collections;
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
    private FoodProcess foodProcess;
    private ITriggerProcress ITriggerProcess;


    private void Awake()
    {
        ITriggerProcess = GetComponent<ITriggerProcress>();
    }
    private void Start()
    {
        foodProcess = new FoodProcess();
    }

    public void DoAction()
    {
        if (isProcessItem) return;
        
        Food foodToProcess = null;
        if (IsContainFood())
        {
            Debug.Log("is Contain food");
            foodToProcess = GetFood();
        }
        else if (IsContainCookware())
        {
            Debug.Log("Is contain cookware");
            foodToProcess = GetCookware().GetFood();
        }
        if (foodToProcess == null) return;
        StartCoroutine(StartConvertNonCookwareFood(() => 
        {
            // cached foodData before process
            var oldFoodData = foodToProcess.GetData();
            foodProcess.ApplyFoodStateChange(foodToProcess, foodStateWantToChange);
            // if process on contain cookware then update food data in cookware
            if (IsContainCookware())
            {
                var cookware = item as Cookware;
                var cookwareHandle = cookware.GetComponent<CookwareRecipeHandle>();
                cookwareHandle.UpdateCurrentFood(foodToProcess.GetData(), oldFoodData);
            }
        }));
    }

    public override void ExchangeItems(HolderAbstract player)
    {
        var canSwap = CanSwap(player);
        if (canSwap == false) return;
        base.ExchangeItems(player);

    }

    private bool CanSwap(HolderAbstract player)
    {
        
        if(type == ContainerType.Cookware && player.IsContainCookware() || type == ContainerType.All)
        {
            return player.GetCookware().GetCookwareType() == cookwareCanPut;
        }
        else if(type == ContainerType.Food && player.IsContainFood() || type == ContainerType.All)
        {
            return foodProcess.CanChangeFoodState(player.GetFood(), foodStateWantToChange);
        }
        return true;
    }

    private IEnumerator StartConvertNonCookwareFood(Action callback)
    {
        if (uiProcessBar != null || isProcessItem) yield break;
        Debug.Log("Start coroutine");
        isProcessItem = true;
        uiProcessBar = UIFoodProcessBarManager.instance.GetUIElement(placeTransform.position);
        bool CanSetModel = true;
        while (timer <= timeToConvert)
        {
            if (CanProcess() == false)
            {
                CanSetModel = false;
                break;
            }

            if (uiProcessBar != null && timer >= 0 && timer <= timeToConvert)
                uiProcessBar.SetFillAmount(timer / timeToConvert);

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (Application.isEditor)
        {
            if (infinityConvert == false && CanSetModel) callback?.Invoke();
        }
        else
        {
            if(CanSetModel)
                callback?.Invoke();
        }

        ResetUIFoodProcess();
    }

    private void ResetUIFoodProcess()
    {
        timer = 0;
        if (uiProcessBar != null)
        {
            uiProcessBar.OnRelease();
        }
        uiProcessBar = null;
        isProcessItem = false;
    }

    private bool CanProcess()
    {
        if (ITriggerProcess == null) return true;
        return ITriggerProcess.OnTrigger;
    }
}

public interface IFoodProcess<T,A> where A : Enum
{
    void ApplyFoodStateChange(T heldItem, A enumType);
}
public interface IConvertible<T,A> where A: Enum
{
    bool CanChangeFoodState(T heldItem, A enumType);
}
public abstract class BaseProcess<T, A> : IFoodProcess<T, FoodState>, IConvertible<T, A> where A : Enum where T : MonoBehaviour
{
    public abstract void ApplyFoodStateChange(T heldItem, FoodState enumType);
    public abstract bool CanChangeFoodState(T heldItem,A enumType);
}
