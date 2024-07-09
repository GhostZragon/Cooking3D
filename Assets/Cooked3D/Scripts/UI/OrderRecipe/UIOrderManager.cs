using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UIOrderManager : ServiceInstaller<UIOrderManager>, ServiceLocator.IGameService
{
    //public static UIOrderManager instance;
    private UnityPool<UIOrderRecipe> UIOrderPool;
    //private IObjectPool<TextMeshProUGUI> textPool;
    [SerializeField] private UIOrderRecipe uIOrderRecipePrefab;
    [SerializeField] private TextMeshProUGUI foodNeedTxtPrefab;
    [SerializeField] private Transform holder;
    [SerializeField] private Transform textHolderPool;
    [SerializeField] private Transform uiHolderPool;

    private UnityPool<UIPrepareFood> UIPrepareFoodPool;
    public UIPrepareFood UIPrepareFoodPrefab;
    public IconManager iconManager;
    protected override void CustomAwake()
    {
        base.CustomAwake();
        //textPool = new ObjectPool<TextMeshProUGUI>(CreateText, OnGet, OnRelease, DestroyText, true, 10);
        UIOrderPool = new UnityPool<UIOrderRecipe>(uIOrderRecipePrefab, 5, holder);

        UIPrepareFoodPool = new UnityPool<UIPrepareFood>(UIPrepareFoodPrefab, 20, holder);
    }

    [Button]
    public UIOrderRecipe InitUIOrder()
    {
        var orderRecipe = UIOrderPool.Get();
        orderRecipe.transform.SetParent(transform);
        orderRecipe.transform.SetParent(holder);
        return orderRecipe;
    }

    public IconManager GetIconManager()
    {
        return iconManager;
    }

    public UIPrepareFood GetUIFood()
    {
        return UIPrepareFoodPool.Get();
    }
}
