using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SourceFoodContainer : BaseContainer<Food>
{
    private BoxCollider BoxCollider;
    [SerializeField] private FoodType FoodType;
    [SerializeField] private List<Food> foodInCrate = new List<Food>();
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float timer;
    private float timeToSpawn = 1;
    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnTest());
    }

    private IEnumerator SpawnTest()
    {
        for (int i = 0; i < 5; i++)
        {
            if (NeedSpawnItem() == false) break;
            SpawnFood();
            yield return new WaitForSeconds(.2f);
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
        var food = FoodManager.instance.GetFoodInstantiate(FoodType, FoodState.Raw);
        food.Init();
        food.transform.SetParent(transform);
        food.transform.localPosition = Vector3.zero;
        food.transform.localPosition = GetRandomSpawnsPosition();
        food.SetStateRb_Col(true,.7f);
        foodInCrate.Add(food);
        timer = 0;
    }

 
    public override void ExchangeItems(HolderAbstract player)
    {
        if (foodInCrate.Count == 0) return;
        if (CanStopContinueSwap(player) == false) return;
        var food = foodInCrate[foodInCrate.Count - 1];
        food.SetStateRb_Col(false,1f);
        player.SetItem(food);
        foodInCrate.Remove(food);
        // Debug.Log("Set food to player");
    }

    private bool NeedSpawnItem() => timer >= timeToSpawn && foodInCrate.Count < maxCount;


    private Vector3 GetRandomSpawnsPosition()
    {
        var minX = BoxCollider.size.x / 2 -.15f;
        var minZ = BoxCollider.size.z / 2 -.15f;
        return new Vector3(Random.Range(-minX, minX), Random.Range(1, 1.3f), Random.Range(-minZ, minZ));
    }
}