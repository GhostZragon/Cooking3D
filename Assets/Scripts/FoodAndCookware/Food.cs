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
    [SerializeField] private FoodData foodData;
    [SerializeField] private Transform model;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
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
        if (model != null)
        {
            Destroy(model.gameObject);
            model = null;
        }
        var mesh = SpawnModel();
        mesh.convex = true;
        mesh.gameObject.layer = LayerMask.NameToLayer("Food");
    }

    private MeshCollider SpawnModel()
    {
        model = Instantiate(foodData.ModelObj, Vector3.zero, Quaternion.identity, transform).transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        return model.AddComponent<MeshCollider>();
    }

    public FoodState GetFoodState()
    {
        return foodData.FoodState;
    }

    public FoodType GetFoodType()
    {
        return foodData.FoodType;
    }


    public void SetStateRb_Col(bool enable, float scaleRatio = 1)
    {
        ChangeStateCollider(enable);
        rb.useGravity = enable;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * scaleRatio;
    }

    private void ChangeStateCollider(bool enable)
    {
        if(model == null) return;   
        model.GetComponent<MeshCollider>().enabled = enable;
    }
    public override void Discard()
    {
        Destroy(gameObject);
    }
}