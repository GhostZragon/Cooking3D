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
    [SerializeField] private Transform container;
    [SerializeField] private GameObject FoodTextGroupObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI recipeNameTxt;
    [SerializeField] private Image fillTimeImage;
    [SerializeField] private Image YesTickImage;
    [SerializeField] private List<TextMeshProUGUI> foodNameTxt;
    public Action<UIOrderRecipe> OnCallback { get; set; }

    private Action OnPopupComplete;
    private Action UpdateLayoutCallback;

    private UIOrderManager UIOrderManagerInstace;

    [SerializeField] private bool showYesTick = false;

    private void Awake()
    {
        foodNameTxt = new List<TextMeshProUGUI>();
        UIOrderManagerInstace = ServiceLocator.Current.Get<UIOrderManager>();
        if (UIOrderManagerInstace == null)
        {
            Debug.LogError("Oh shitt it null");
        }
    }
    public void UpdateDataFromRecipe(Recipes recipes)
    {
        recipeNameTxt.text = recipes.name;
        var listFood = recipes.GetRequiredIngredients().GetIngredientQuantities();
        var foodNeedCount = recipes.FoodNeedCount;
     
        foreach (var food in listFood)
        {
            //var text = UIOrderManager.instance.textPool.Get();
            var text = UIOrderManagerInstace.GetTextFromPool();
            if (text == null) continue;
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
            UIOrderManagerInstace.ReleaseTextToPool(text);
        }
       
        foodNameTxt.Clear();
        OnCallback?.Invoke(this);
        transform.DOKill();
    }

    public void SetCreationAnimationCallback(Action callback)
    {
        OnPopupComplete = callback;
    }

    public void SetDeletionAnimationCallback(Action callback)
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
    }
    public Ease easeYesTickRotate;
    private IEnumerator HideCoroutine()
    {
        if (showYesTick)
        {
            var yesTickTransform = YesTickImage.transform;
            YesTickImage.transform.localScale = Vector2.one * 2;
            YesTickImage.DOFade(0, 0);
            YesTickImage.DOFade(1, .3f);
            yield return yesTickTransform.DOScale(Vector2.one * 1, .3f).SetEase(Ease.OutBounce).WaitForCompletion();
            yesTickTransform.DOScale(Vector2.one * 1.2f, .5f).SetEase(Ease.OutBounce);
            
            yield return new WaitForSeconds(.1f);
            yield return yesTickTransform.DOPunchRotation(new Vector3(0,0,10), 1.5f).SetEase(easeYesTickRotate).WaitForCompletion();
            yield return yesTickTransform.DOScale(Vector2.one * .9f, .2f).WaitForCompletion();
            yield return new WaitForSeconds(.3f);
        }
        // Move panel by y asix
        container.transform.DOLocalMoveY(125, .5f).SetEase(Ease.OutBack);
        // show yes tick control by game logic: Time out == false, give correct recipe == true
        

        yield return new WaitForSeconds(.1f);

        canvasGroup.DOFade(0, .2f).OnComplete(() =>
        {
            //ResetToInitState();
            UpdateLayoutCallback?.Invoke();

            ResetToInitState();
            OnRelease();

        });
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
        Debug.Log("Reset state");
        container.DOKill();
        container.localPosition = Vector2.zero;
       
        recipeNameTxt.transform.localScale = Vector2.zero;
        
        fillTimeImage.fillAmount = 0;
        
        container.localScale = Vector2.zero;

        YesTickImage.transform.localScale = Vector2.zero;
        Debug.Log("Container position: " + container.localPosition);
        showYesTick = false;
    }

    public void ShowYesTick() => showYesTick = true;
    public void HideYesTick() => showYesTick = false;
    [Button]
    private void TestRotate()
    {
        var finalValue = container.transform.localPosition.y + 135;
        container.transform.DOJump(new Vector3(container.transform.localPosition.x, finalValue, container.transform.localPosition.z), 1, 1, .5f);
    }
}
