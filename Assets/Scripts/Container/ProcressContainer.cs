using NaughtyAttributes;
using UnityEngine;

public interface IOnDoAction
{
    void DoAction();
}
public class ProcressContainer : HolderAbstract, IOnDoAction
{
    [SerializeField] private FoodState foodStateWantToChange;
    [SerializeField] private float timer;
    [SerializeField] private bool canTimer;
    [SerializeField] private bool isConvert;
    [SerializeField] private float timeToConvert = 1;

    private void Update()
    {
        if (canTimer)
        {
            timer += Time.deltaTime;
            if (timer >= timeToConvert)
            {
                Convert();
            }
        }
    }
    
    void StartTimer()
    {
        if (item != null)
        {
            canTimer = true;
        }
    }
    
    [Button]
    private void Convert()
    {
        canTimer = false;
        if (item == null || item is Cookware) return;
        var food = GetFood();
        var foodData = FoodManager.instance.GetFoodData(food.GetFoodType(), foodStateWantToChange);
        if (foodData == null) return;
        food.SetData(foodData);
        food.SetModel();
    }

    public void DoAction()
    {
        StartTimer();
    }
}