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
    [SerializeField] private List<Food> foodInCrate = new List<Food>();
    private AudioSource audioSource;
    private BoxCollider BoxCollider;
    private FoodManager foodManager;

    private IGameControl IgameControl;

    private float timeToSpawn = 1;
    public void ExchangeItems(HolderAbstract player)
    {
        if (foodInCrate.Count == 0) return;
        if (player.IsContainFood()) return;

        var cookware = player.GetCookware();
        player.SetItem(GetFoodInList());
        audioSource.Play();
        // Debug.Log("Set food to player");
    }

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
        foodManager = ServiceLocator.Current.Get<FoodManager>();
        IgameControl = ServiceLocator.Current.Get<GameControl>();
        audioSource = GetComponent<AudioSource>();
    }   


    private void Update()
    {
        if (IgameControl.canSpawnFood == false) return;

        if (timer >= timeToSpawn && foodInCrate.Count < maxCount)
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
        var newFood = CreateFood();
        if (newFood == null) return;
        foodInCrate.Add(newFood);
        timer = 0;
    }
    private Food GetFoodInList()
    {
        if (foodInCrate.Count == 0) return null;

        var food = foodInCrate[foodInCrate.Count - 1];
        food.SetStateRb_Col(false);
        foodInCrate.Remove(food);
        return food;
    }

    private Food CreateFood()
    {
        if(FoodType == FoodType.None)
        {
            Debug.LogWarning("This food container is null", gameObject);
            return null;
        }

        var food = foodManager.GetFoodInstantiate(FoodType, FoodState.Raw);
        
        if(food == null) return null;
        
        food.Init();
        food.SetToParentAndPosition(transform);
        food.transform.localPosition = GetRandomSpawnsPosition();
        food.SetStateRb_Col(true);
        return food;
    }

    private Vector3 GetRandomSpawnsPosition()
    {
        var minX = BoxCollider.size.x / 2 - .15f;
        var minZ = BoxCollider.size.z / 2 - .15f;
        return new Vector3(Random.Range(-minX, minX), Random.Range(1, 1.3f), Random.Range(-minZ, minZ));
    }
}