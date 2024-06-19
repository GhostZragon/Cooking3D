using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum FoodType
{
    // Burger
    None,
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
    Cooked,
    Mixed
}

[Serializable]
public struct RuntimeFoodData
{
    [FormerlySerializedAs("prepareTechniques")]
    public FoodState foodState;

    public GameObject Model;
}

public class Food : PickUpAbtract
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider[] Colliders;
    [SerializeField] private FoodData foodData;
    [SerializeField] private Transform model;
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
        if (data == null) return;
        this.foodData = data;
    }

    public void SetModel()
    {
        if (foodData == null) return;
        if (model != null) DestroyModel();
        model = Instantiate(foodData.ModelObj, Vector3.zero, Quaternion.identity, transform).transform;
        model.transform.localPosition = Vector3.zero;
        var mesh = model.AddComponent<MeshCollider>();
        mesh.convex = true;
        mesh.gameObject.layer = LayerMask.NameToLayer("Food");
        
    }
    private void DestroyModel()
    {
        Destroy(model.gameObject);
        model = null;
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * scaleRatio;
    }

}