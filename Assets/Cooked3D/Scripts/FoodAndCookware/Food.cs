using System;
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
public class Food : PickUpAbtract, PoolCallback<Food>
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FoodData foodData;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private FoodCustomizeMesh foodCustomizeMesh;

    public Action<Food> OnCallback { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        foodCustomizeMesh = GetComponent<FoodCustomizeMesh>();
    }

    public void Init()
    {
        SetStateRb_Col(false, .7f);
    }

    public void SetData(FoodData data)
    {
        if (data == null) return;
        this.foodData = data;
        transform.name = $"Food_{data.FoodType.ToString()}_{data.FoodState.ToString()}";
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
        meshCollider.enabled = enable;

        rb.useGravity = enable;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * scaleRatio;
    }


    public override void Discard()
    {
        OnCallback?.Invoke(this);
    }

    public FoodData GetData()
    {
        return foodData;
    }

    internal void SetModel()
    {
        foodCustomizeMesh.SetMesh(foodData.GetMesh());
    }

    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }
}