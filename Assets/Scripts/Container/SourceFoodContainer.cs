using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class SourceFoodContainer : BaseContainer<Food>
{
    public FoodType FoodType;
    public ContainerBuilderManager builder;
    public GameObject baseModel;

    private void Awake()
    {
        Init();
    }

    [Button]
    private void Init()
    {
        foreach (var container in builder.foodList)
        {
            if (container.foodType == FoodType)
            {
                // baseModel.SetActive(false);
                // var containerGo = Instantiate(container.Model);
                // containerGo.transform.SetParent(transform);
                // containerGo.transform.localPosition = Vector3.zero;
                prefab = container.Prefab;
                break;
            }
        }
    }
    
    [Button]
    private void SpawnFood()
    {
        var food = RetrieveRawFood();
        food.transform.SetParent(transform);
        food.transform.localPosition = Vector3.zero;
        food.transform.localPosition += Vector3.up;
        foodInCrate.Add(food);
    }
    [SerializeField] private List<Food> foodInCrate = new List<Food>();
    [SerializeField] private int maxCount = 5;
    public override void ExchangeItems(HolderAbstract holder)
    {
        if (foodInCrate.Count == 0) return;
        if (holder.IsContainFood() || holder.IsContainPlate()) return;
        var food = foodInCrate[foodInCrate.Count - 1];
        food.SetStateRb_Col(false);
        holder.SetFood(food);
        foodInCrate.Remove(food);
        Debug.Log("Set food to holder");
    }

  
}