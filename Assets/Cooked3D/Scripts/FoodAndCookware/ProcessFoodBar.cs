using NaughtyAttributes;
using System;
using UnityEngine;

public class ProcessFoodBar : MonoBehaviour
{
    public enum BarEnum
    {
        None,
        Good,
        Bad
    }
    [SerializeField,MinValue(0),MaxValue(2)] private float processValue;
    [SerializeField] private BarEnum barEnum;
    public Action<float> onChangeValue;
    public void UpdateBar(float value)
    {
        if(onChangeValue == null)
        {
            // init UI progress bar here
        }
        processValue = value;
        onChangeValue?.Invoke(processValue);
    }

    public BarEnum GetBarEnum()
    {
        return barEnum;
    }
}
