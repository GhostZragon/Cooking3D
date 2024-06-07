using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
public class Food : PickUpAbtract
{
    [SerializeField] private List<RuntimeFoodData> TransformFoods;
    public FoodType type;
    public int enumIndex;
    private void Awake()
    {
        ChangePrepareTechniques(0);
    }

    public PrepareTechniques GetPrepareTech()
    {
        return (PrepareTechniques)enumIndex;
    }
    public void ChangePrepareTechniques(PrepareTechniques prepareTechniques)
    {
        foreach (var item in TransformFoods)
        {
            if (item.prepareTechniques == prepareTechniques)
            {
                Debug.Log("Food contain "+prepareTechniques.ToString());
                break;
            }
        }
        for (int i = 0; i < TransformFoods.Count; i++)
        {
            var item = TransformFoods[i];
            if (prepareTechniques == item.prepareTechniques)
            {
                item.Model.SetActive(true);
                enumIndex = (int)item.prepareTechniques;
            }
            else
            {
                item.Model.SetActive(false);
            }
        }

    }
}
