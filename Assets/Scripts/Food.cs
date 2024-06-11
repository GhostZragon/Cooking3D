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
    [SerializeField] private List<RuntimeFoodData> TransformFoods;
    [SerializeField] private  FoodType type;
    [SerializeField] private  int enumIndex;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider[] Colliders;
    [SerializeField] private PhysicMaterial foodPhyicsMaterial;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
        AddPhycsMaterial();
        ChangeFoodState(0);
        transform.localScale = Vector3.one * .7f;
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
        return (FoodState)enumIndex;
    }

    private void ChangeFoodState(FoodState foodState)
    {
        // if (TransformFoods.Any(item => item.foodState == foodState))
        // {
        //     Debug.Log("Food contain "+foodState.ToString());
        // }
        foreach (var item in TransformFoods)
        {
            if (foodState == item.foodState)
            {
                item.Model.SetActive(true);
                enumIndex = (int)item.foodState;
            }
            else
            {
                item.Model.SetActive(false);
            }
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetStateRb_Col(bool enable)
    {
        rb.useGravity = enable;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; 
        ChangeCollidersState(enable);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = Vector3.one;
    }

    public FoodType GetFoodType()
    {
        return type;
    }

    public List<FoodState> GetFoodStateList()
    {
        var list = new List<FoodState>();
        foreach (var item in TransformFoods)
        {
            list.Add(item.foodState);
        }
        return list;
    }
}
