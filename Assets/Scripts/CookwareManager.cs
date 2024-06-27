using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareManager : MonoBehaviour
{
    public static CookwareManager instance;
    [SerializeField] private Material cookwareMat;
    [SerializeField] private Cookware cookwarePrefab;
    [SerializeField] private bool scanWhenStart;

    public Dictionary<CookwareType, CookwareLimit> cookwareLitmitDict;

    private void Awake()
    {
        instance = this;
        InitCookwareLimit();
        CountingCookwareInGame();
    }

    private void CountingCookwareInGame()
    {
        if (scanWhenStart == false) return;

        var cookwaresInGame = GameObject.FindObjectsOfType<Cookware>();
        foreach(var item in cookwaresInGame)
        {
            var type = item.GetCookwareType();
            if (cookwareLitmitDict.ContainsKey(type))
            {
                cookwareLitmitDict[type].Increase();
            }
        }
    }

    private void OnValidate()
    {
        if (cookwareMat == null)
        {
            cookwareMat = Resources.Load<Material>(ConstantPath.Resource.COOKING_MATERIAL);
        }
    }
    private void InitCookwareLimit()
    {
        cookwareLitmitDict = new Dictionary<CookwareType, CookwareLimit>()
        {
            {CookwareType.Plate,new CookwareLimit(0,3) },
            {CookwareType.Pan,new CookwareLimit(0,3) },
            {CookwareType.Pot,new CookwareLimit(0,3) }
        };
    }
    public Cookware GetCookware(CookwareType type)
    {
        if (cookwarePrefab == null)
        {
            Debug.Log("Cookware prefab is null", gameObject);
            return null;
        }

        var cookware = Instantiate(cookwarePrefab);
        var meshCustomize = cookware.GetComponent<MeshCustomize>();
        cookware.SetCookwareType(type);
        return cookware;
    }

    internal bool CanPutFoodInCookware(CookwareType type, Food food)
    {
        if(type == CookwareType.Pan)
        {
            var foodData = FoodManager.instance.GetFoodData(food.GetFoodType(), FoodState.Cooked);
            return foodData != null;
        }
        return false;
    }
}
