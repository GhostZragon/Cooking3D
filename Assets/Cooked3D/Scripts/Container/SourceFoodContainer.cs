using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SourceFoodContainer : MonoBehaviour, IHolder
{
    [SerializeField] private FoodType FoodType;
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float timer;

    private BoxCollider BoxCollider;
    private float timeToSpawn = 1;
    [SerializeField] private List<Food> foodInCrate = new List<Food>();
    private FoodManager foodManager;
    public void ExchangeItems(HolderAbstract player)
    {
        if (foodInCrate.Count == 0) return;
        if (player.IsContainFood() || player.IsContainCookware()) return;
        player.SetItem(GetFoodInList());
        // Debug.Log("Set food to player");
    }

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
        //StartCoroutine(SpawnTest());
    }
    private void Start()
    {
        foodManager = FoodManager.instance;
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
    private bool NeedSpawnItem() => timer >= timeToSpawn && foodInCrate.Count < maxCount;

    [Button]
    private void SpawnFood()
    {
        if (foodInCrate.Count == maxCount) return;
        var newFood = CreateFood();
        if (newFood == null) return;
        foodInCrate.Add(newFood);
        timer = 0;
    }
    private Food GetFoodInList()
    {
        var food = foodInCrate[foodInCrate.Count - 1];
        food.SetStateRb_Col(false, 1f);
        foodInCrate.Remove(food);
        return food;
    }

    private Food CreateFood()
    {
        var food = foodManager.GetFoodInstantiate(FoodType, FoodState.Raw);
        if(food == null) return null;
        food.Init();
        food.SetToParentAndPosition(transform);
        food.transform.localPosition = GetRandomSpawnsPosition();
        food.SetStateRb_Col(true, .7f);
        return food;
    }

    private Vector3 GetRandomSpawnsPosition()
    {
        var minX = BoxCollider.size.x / 2 - .15f;
        var minZ = BoxCollider.size.z / 2 - .15f;
        return new Vector3(Random.Range(-minX, minX), Random.Range(1, 1.3f), Random.Range(-minZ, minZ));
    }
}