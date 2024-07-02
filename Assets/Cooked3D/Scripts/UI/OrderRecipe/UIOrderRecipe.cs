using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private CanvasGroup canvasGroup;
    public Action<UIOrderRecipe> OnCallback { get; set; }

    private Action PopupAction;
    private Action PopinAction;
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
        PopupAction = callback;
    }

    public void SetPopInCallback(Action callback)
    {
        PopinAction = callback;
    }
    [Button]
    public void Show()
    {
        StartCoroutine(ShowCoroutine(1,Vector3.one,1));
    }

    [Button]
    public void Hide()
    {
        StartCoroutine(HideCoroutine());
        //container.transform.DOScale(Vector3.zero, .1f);
        //container.transform.localScale = Vector3.zero;
    }
    private IEnumerator HideCoroutine()
    {
        container.transform.DOLocalMoveY(125, .5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            container.localPosition = Vector3.zero;
            recipeNameTxt.transform.localScale = Vector3.zero;
            fillTimeImage.fillAmount = 0;
            container.localScale = Vector3.zero;
        });
        yield return new WaitForSeconds(.1f);
        canvasGroup.DOFade(0, .2f);
    }
    private IEnumerator ShowCoroutine(float fadeValue, Vector3 scale, float fillAmount)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(container.DOScale(Vector3.one, .3f)).SetEase(Ease.OutBack);
        //sequence.Append(container.DOShakeScale(.3f, .1f));
        sequence.Join(canvasGroup.DOFade(fadeValue, .1f));
        sequence.Play();
        //yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(.1f);
        yield return recipeNameTxt.transform.DOScale(scale, .2f).SetEase(Ease.OutElastic).WaitForCompletion();
        yield return fillTimeImage.DOFillAmount(fillAmount, .2f).SetEase(Ease.InOutCubic).WaitForCompletion();
        PopupAction?.Invoke();
    }

}
