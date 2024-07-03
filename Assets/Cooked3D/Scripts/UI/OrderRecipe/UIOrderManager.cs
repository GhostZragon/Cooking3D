using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UIOrderManager : ServiceInstaller<UIOrderManager>, ServiceLocator.IGameService
{
    //public static UIOrderManager instance;
    private UnityPool<UIOrderRecipe> UIOrderPool;
    private IObjectPool<TextMeshProUGUI> textPool;
    [SerializeField] private UIOrderRecipe uIOrderRecipePrefab;
    [SerializeField] private TextMeshProUGUI foodNeedTxtPrefab;
    [SerializeField] private Transform holder;
    [SerializeField] private Transform textHolderPool;
    [SerializeField] private Transform uiHolderPool;
  

    protected override void CustomAwake()
    {
        base.CustomAwake();
        textPool = new ObjectPool<TextMeshProUGUI>(CreateText, OnGet, OnRelease, DestroyText, true, 10);
        UIOrderPool = new UnityPool<UIOrderRecipe>(uIOrderRecipePrefab, 5, holder);
    }

    [Button]
    public UIOrderRecipe InitUIOrder()
    {
        var orderRecipe = UIOrderPool.Get();
        orderRecipe.transform.SetParent(transform);
        orderRecipe.transform.SetParent(holder);
        return orderRecipe;
    }




    private TextMeshProUGUI CreateText()
    {
        return Instantiate(foodNeedTxtPrefab, transform);
    }
    private void OnGet(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }
    private void OnRelease(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
        text.transform.SetParent(textHolderPool, false);
    }
    private void DestroyText(TextMeshProUGUI text)
    {
        Destroy(text.gameObject);
    }

    public void ReleaseTextToPool(TextMeshProUGUI text)
    {
        textPool.Release(text);
    }

    public TextMeshProUGUI GetTextFromPool()
    {
        return textPool.Get();
    }

}
