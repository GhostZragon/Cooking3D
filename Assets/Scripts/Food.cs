using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    // Burger
    Tomato,
    Chesse,
    Buns,
    BurgerMeet
}

public enum PrepareTechniques
{
    Raw,
    Fry,
    Chop
}
[Serializable]
public struct RuntimeFoodData
{
    public PrepareTechniques prepareTechniques;
    public GameObject Model;
}
public class Food : MonoBehaviour
{
    public FoodType type;
    public List<RuntimeFoodData> TransformFoods;

    private void Awake()
    {
        foreach(var item in TransformFoods)
        {
            if(item.prepareTechniques != PrepareTechniques.Raw)
            {
                item.Model.SetActive(false);
            }
        }
    }
}
