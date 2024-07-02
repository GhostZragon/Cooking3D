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

    private Action OnPopupComplete;
    private Action UpdateLayoutCallback;
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
            text.transform.localScale = Vector2.zero;
            foodNameTxt.Add(text);
        }
        ResetToInitState();
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

    public void SetPopUpDoneCallBack(Action callback)
    {
        OnPopupComplete = callback;
    }

    public void SetUpdateLayoutCallback(Action callback)
    {
        UpdateLayoutCallback = callback;
    }
    [Button]
    public void Show()
    {
        StartCoroutine(ShowCoroutine());
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
            //ResetToInitState();
            UpdateLayoutCallback?.Invoke();
            OnRelease();
        });
        yield return new WaitForSeconds(.1f);
        canvasGroup.DOFade(0, .2f);
    }
    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(container.DOScale(Vector2.one, .3f)).SetEase(Ease.OutBack);
        //sequence.Append(container.DOShakeScale(.3f, .1f));
        sequence.Join(canvasGroup.DOFade(1, .3f));
        sequence.Play();
        //yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(.2f);
        yield return recipeNameTxt.transform.DOScale(Vector2.one, .3f).SetEase(Ease.OutElastic).WaitForCompletion();
        foreach(var item in foodNameTxt)
        {
            item.transform.DOScale(Vector2.one, .1f);
        }
        yield return fillTimeImage.DOFillAmount(1, .3f).SetEase(Ease.InOutCubic).WaitForCompletion();
        OnPopupComplete?.Invoke();
    }
    private void ResetToInitState()
    {
        container.localPosition = Vector2.zero;
        recipeNameTxt.transform.localScale = Vector2.zero;
        fillTimeImage.fillAmount = 0;
        container.localScale = Vector2.zero;
    }
}
