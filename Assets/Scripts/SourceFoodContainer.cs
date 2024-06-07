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
                baseModel.SetActive(false);
                var containerGo = Instantiate(container.Model);
                containerGo.transform.SetParent(transform);
                containerGo.transform.localPosition = Vector3.zero;
                prefab = container.Prefab;
                break;
            }
        }
    }
}