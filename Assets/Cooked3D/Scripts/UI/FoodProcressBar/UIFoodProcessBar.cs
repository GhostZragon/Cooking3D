using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodProcessBar : UIWorldSpace, PoolCallback<UIFoodProcessBar>
{
    [SerializeField] private Image fillImg;
    [SerializeField] private Image Background;

    public Action<UIFoodProcessBar> OnCallback { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }
    [Button]
    public void Show()
    {
        SetActive(true);
    }

    [Button]
    public void Hide()
    {
        SetActive(false);
    }

    private void SetActive(bool enable)
    {
        fillImg.gameObject.SetActive(enable);
        Background.gameObject.SetActive(enable);
    }

    public void SetFillAmount(float amount)
    {
        fillImg.fillAmount = amount;
    }

    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }
}
