﻿using System;
using UnityEngine;

[Serializable] 
public class IngredientQuantity
{
    public IngredientQuantity(FoodData foodData, int amout)
    {
        this.foodData = foodData;
        this.amount = amout;
    }
    [SerializeField] private FoodData foodData;
    [SerializeField] private int amount;
    public int Amount { get => amount; }
    public FoodData FoodData { get => foodData; }
    public bool CanAddMore(IngredientQuantity ingredientQuantity)
    {
        return ingredientQuantity.amount < amount;
    }

    public void SetFoodData(FoodData foodData)
    {
        this.foodData = foodData;
    }
    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
    
}