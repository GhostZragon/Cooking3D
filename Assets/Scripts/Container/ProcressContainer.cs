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
    private IFoodProcess<Food> foodProcess;
    private ITriggerProcress ITriggerProcess;


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
            StartCoroutine(StartConvert(() => { foodProcess.ChangeFoodState(GetFood()); }));
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
                uiProcessBar.SetFillAmount(timer / timeToConvert);

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (Application.isEditor)
        {
            if (infinityConvert == false) callback?.Invoke();
        }
        else
        {
            callback?.Invoke();
        }

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

public interface IFoodProcess<T>
{
    bool IsConvertible(T heldItem);
    void ChangeFoodState(T heldItem);
}