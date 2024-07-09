using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class UIPrepareFood : MonoBehaviour, PoolCallback<UIPrepareFood>
{
    [SerializeField] private Image foodImg;
    [SerializeField] private Image subIcon;
    public Action<UIPrepareFood> OnCallback { get; set; }
    private IconManager iconManager;
    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }
    private void Awake()
    {
        iconManager = ServiceLocator.Current.Get<UIOrderManager>().GetIconManager();
    }
    public void SetIcon(FoodData foodData)
    {
        foodImg.sprite = iconManager.GetIcon(foodData.FoodType);
        OnShow(foodImg);
        subIcon.sprite = iconManager.GetIcon(foodData.FoodState);
        OnShow(subIcon);
    }

    private void OnShow(Image image)
    {
        if (image.sprite == null)
        {
            image.DOFade(0, 0);
        }
        else
        {
            image.DOFade(1, 0);
        }
    }
}
