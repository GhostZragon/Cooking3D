using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Tomatoe,
    Chesse,
    Buns,
    Steak,
    Carrot,
}

public enum PrepareTechniques
{
    Slice,
    Mince,
    Dice,
    Grind
}

public class Food : MonoBehaviour
{
    public FoodType type;
}
