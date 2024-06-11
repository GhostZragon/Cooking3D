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
        ChangePrepareTechniques(0);
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
    public PrepareTechniques GetPrepareTech()
    {
        return (PrepareTechniques)enumIndex;
    }

    private void ChangePrepareTechniques(PrepareTechniques prepareTechniques)
    {
        // if (TransformFoods.Any(item => item.prepareTechniques == prepareTechniques))
        // {
        //     Debug.Log("Food contain "+prepareTechniques.ToString());
        // }

        foreach (var item in TransformFoods)
        {
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

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetStateRb_Col(bool enable)
    {
        rb.useGravity = enable;
        ChangeCollidersState(enable);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = Vector3.one;
    }

    public FoodType GetFoodType()
    {
        return type;
    }

    public List<RuntimeFoodData> GetRuntimeFoodData()
    {
        return TransformFoods;
    }
}
