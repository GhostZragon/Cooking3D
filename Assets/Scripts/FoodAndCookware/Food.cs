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
    [SerializeField] private PhysicMaterial foodPhyicsMaterial;
    [SerializeField] private FoodData foodData;
    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
        AddPhycsMaterial();
        SetStateRb_Col(false, .7f);
    }
    private void AddPhycsMaterial()
    {
        if (Colliders == null) return;
        foreach (var col in Colliders)
        {
            col.material = foodPhyicsMaterial;
        }
    }

    private void ChangeCollidersState(bool enable)
    {
        foreach (var col in Colliders)
        {
            col.enabled = enable;
        }
    }
    public FoodState GetCurrentFoodState()
    {
        return foodData.FoodState;
    }
    public FoodType GetFoodType()
    {
        return foodData.FoodType;
    }
    private void ChangeFoodState(FoodState foodState)
    {

    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetStateRb_Col(bool enable, float scaleRatio = 1)
    {
        rb.useGravity = enable;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; 
        ChangeCollidersState(enable);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = Vector3.one * scaleRatio;
    }


    public void SetData(FoodData data)
    {
        this.foodData = data;
    }
}
