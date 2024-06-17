using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public enum FoodType
{
    // Burger
    Tomato,
    Chesse,
    Buns,
    BurgerMeet,
    Steak
}


public enum FoodState
{
    Raw,
    Fry,
    Slice,
    Cooked
}
[Serializable]
public struct RuntimeFoodData
{
    [FormerlySerializedAs("prepareTechniques")] public FoodState foodState;
    public GameObject Model;
}
public class Food : PickUpAbtract
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider[] Colliders;
    [SerializeField] private FoodData foodData;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        Colliders = GetComponentsInChildren<Collider>();
        SetStateRb_Col(false, .7f);
    }

    public void SetData(FoodData data)
    {
        this.foodData = data;
    }
    public FoodState GetCurrentFoodState()
    {
        return foodData.FoodState;
    }
    public FoodType GetFoodType()
    {
        return foodData.FoodType;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetStateRb_Col(bool enable, float scaleRatio = 1)
    {
        foreach (var col in Colliders) col.enabled = enable;
        rb.useGravity = enable;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; 
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = Vector3.one * scaleRatio;
    }

}
