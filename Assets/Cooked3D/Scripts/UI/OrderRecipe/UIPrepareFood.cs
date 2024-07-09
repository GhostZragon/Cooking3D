using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class UIPrepareFood : MonoBehaviour, PoolCallback<UIPrepareFood>
{
    public Image foodImg;
    public Image subIcon;
    public Action<UIPrepareFood> OnCallback { get; set; }

    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }

    internal void SetIcon(FoodData foodData)
    {
        var iconManager = ServiceLocator.Current.Get<UIOrderManager>().GetIconManager();
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
