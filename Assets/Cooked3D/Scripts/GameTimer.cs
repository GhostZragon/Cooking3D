using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int timer;
    [SerializeField] private int endTime = 300;
    YieldInstruction yieldWaitForSecond;
    public static Action<int> OnUpdateTimer;
    public static Action StartCounter;
    public static Action StopCounter;
    private void Awake()
    {
        yieldWaitForSecond = new WaitForSeconds(1);
    }
    private void OnEnable()
    {
        StartCounter += BeginTimer;
        StopCounter += StopTimer;
    }
    private void OnDisable()
    {
        StartCounter -= BeginTimer;
        StopCounter -= StopTimer;
    }
    [Button]
    private void BeginTimer()
    {
        StartCoroutine(StartTimer());
    }
    [Button]
    public void StopTimer()
    {
        StartCoroutine(StartTimer());
    }
    private IEnumerator StartTimer()
    {
        while(timer < endTime)
        {
            timer++;
            OnUpdateTimer?.Invoke(timer);
            yield return yieldWaitForSecond;
        }
    }
}
