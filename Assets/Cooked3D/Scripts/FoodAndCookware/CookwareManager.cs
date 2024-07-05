using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareManager : ServiceInstaller<CookwareManager> ,ServiceLocator.IGameService
{
    [SerializeField] private Material cookwareMat;
    [SerializeField] private Cookware cookwarePrefab;
    [SerializeField] private bool scanWhenStart;

    public Dictionary<CookwareType, CookwareLimit> cookwareLitmitDict;
    public Cookware[] cookwareInGames;
    private FoodManager foodManager;
    protected override void CustomAwake()
    {
        base.CustomAwake();
        InitCookwareLimit();
        CountingCookwareInGame();
    }
    private void Start()
    {
        foodManager = ServiceLocator.Current.Get<FoodManager>();
        cookwareInGames = FindObjectsOfType<Cookware>();
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
        if (food == null) return true;
        if (type == CookwareType.Pan)
        {
            var foodData = foodManager.GetFoodData(food.GetFoodType(), FoodState.Cooked);
            return foodData != null;
        }
        return false;
    }
#if UNITY_EDITOR
    [Header("Testing")]
    [SerializeField]
    private CookwareType cookwareTypeWantSpawn;
    [Button]
    private void TestingSupport()
    {
        GetCookware(cookwareTypeWantSpawn);
    }
#endif
}
