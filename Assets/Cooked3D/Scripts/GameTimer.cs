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
    private void Awake()
    {
        yieldWaitForSecond = new WaitForSeconds(1);
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
