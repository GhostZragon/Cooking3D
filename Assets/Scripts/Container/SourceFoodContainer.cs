using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class SourceFoodContainer : BaseContainer<Food>
{
    private BoxCollider BoxCollider;
    [SerializeField] private FoodType FoodType;
    [SerializeField] private ContainerBuilderManager builder;
    [SerializeField] private GameObject baseModel;
    [SerializeField] private List<Food> foodInCrate = new List<Food>();
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float timer;
    private float timeToSpawn = 1;
    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
        Init();
    }

    [Button]
    private void Init()
    {
        foreach (var container in builder.foodList)
        {
            if (container.foodType == FoodType)
            {
                prefab = container.Prefab;
                break;
            }
        }
    }

    private void Update()
    {
        if (NeedSpawnItem())
        {
            timer = 0;
            SpawnFood();
        }
        
        if (timer <= timeToSpawn)
        {
            timer += Time.deltaTime;
        }
        
    }

    [Button]
    private void SpawnFood()
    {
        if (foodInCrate.Count == maxCount) return;
        var food = RetrieveRawFood();
        food.transform.SetParent(transform);
        food.transform.localPosition = Vector3.zero;
        food.transform.localPosition = GetRandomSpawnsPosition();
        food.SetStateRb_Col(true,.7f);
        foodInCrate.Add(food);

    }
  
    public override void ExchangeItems(HolderAbstract holder)
    {
        if (foodInCrate.Count == 0) return;
        if (CanStopContinueSwap(holder)) return;
        var food = foodInCrate[foodInCrate.Count - 1];
        food.SetStateRb_Col(false);
        holder.SetFood(food);
        foodInCrate.Remove(food);
        Debug.Log("Set food to holder");
    }

    private bool NeedSpawnItem() => timer >= timeToSpawn && foodInCrate.Count < maxCount;


    private Vector3 GetRandomSpawnsPosition()
    {
        var minX = BoxCollider.size.x / 2 -.15f;
        var minZ = BoxCollider.size.z / 2 -.15f;
        return new Vector3(Random.Range(-minX, minX), Random.Range(1, 1.3f), Random.Range(-minZ, minZ));
    }
}