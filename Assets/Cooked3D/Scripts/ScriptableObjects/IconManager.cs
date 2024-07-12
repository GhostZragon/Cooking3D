using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Icon Manager",menuName = "ScriptableObjects/Icon Manager")]
public class IconManager : ScriptableObject
{
    [SerializeField] private List<FoodIcon> foodTypes;
    [SerializeField] private List<FoodStateIcon> cookwareIcon;
    /// <summary>
    /// Get sprite of what state of food, like Cooked, Slice
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Sprite GetIcon(FoodState type)
    {
        foreach (var item in cookwareIcon)
        {
            if (item.foodState == type)
                return item.sprite;
        }
        return null;
    }
    /// <summary>
    /// Get sprite of food icon
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Sprite GetIcon(FoodType type)
    {
        foreach(var item in foodTypes)
        {
            if (item.foodType == type)
                return item.sprite;
        }
        return null;
    }
}
[Serializable]
public class FoodIcon
{
    public FoodIcon(FoodType foodType)
    {
        this.foodType = foodType;
    }
    public FoodType foodType;
    public Sprite sprite;
}
[Serializable]
public class FoodStateIcon
{
    public FoodStateIcon(FoodState type)
    {
        this.foodState = type;
    }
    public FoodState foodState;
    public Sprite sprite;
}