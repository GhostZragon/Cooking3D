using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeOrderHandle : MonoBehaviour
{
    [SerializeField] private RecipeOrderProcessor orderProcessor;
    [SerializeField] private int maxOrderInPlayTime;

    [SerializeField] private bool needSpawnOrder;

    [SerializeField] private float timer;
    [SerializeField] private float maxCounterTime;
    [SerializeField] private float minCounterTime;
    [SerializeField] private float timeBetweenOrderSpawned;
    [SerializeField] private float spawnTimeThreshold;


    private void Counting()
    {
        if (orderProcessor == null) return;


        timer += Time.deltaTime;
        if(timer > timeBetweenOrderSpawned && needSpawnOrder)
        {
            // Spawn
            orderProcessor.CreateOrder();
        }
    }
}
