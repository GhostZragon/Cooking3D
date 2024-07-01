using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderRecipe : MonoBehaviour, PoolCallback<UIOrderRecipe>
{
    [SerializeField] private List<TextMeshProUGUI> foodNameTxt;
    [SerializeField] private TextMeshProUGUI recipeNameTxt;
    [SerializeField] private Image fillTimeImage;
    [SerializeField] private GameObject FoodTextGroupObject;
    [SerializeField] private Transform container;
    public Action<UIOrderRecipe> OnCallback { get; set; }
    
    private void Awake()
    {
        foodNameTxt = new List<TextMeshProUGUI>();
    }
    public void UpdateDataFromRecipe(Recipes recipes)
    {
        recipeNameTxt.text = recipes.name;
        var listFood = recipes.GetRequiredIngredients().GetIngredientQuantities();
        var foodNeedCount = recipes.FoodNeedCount;
     
        foreach (var food in listFood)
        {
            var text = UIOrderManager.instance.textPool.Get();
            text.transform.SetParent(FoodTextGroupObject.transform, false);
            text.gameObject.SetActive(true);
            text.text = food.FoodData.name;
            text.transform.localScale = Vector3.zero;
            foodNameTxt.Add(text);
        }
    }
    public void UpdateFillValue(float value)
    {
        fillTimeImage.fillAmount = value;
    }
    [Button]
    public void OnRelease()
    {
        foreach (var text in foodNameTxt)
        {
            UIOrderManager.instance.textPool.Release(text);
        }
        foodNameTxt.Clear();
        OnCallback?.Invoke(this);
    }

    public void SetPopUpCallBack(Action callback)
    {
        callback?.Invoke();
    }

    public void SetPopInCallback(Action callback)
    {
        callback?.Invoke();
    }
}
