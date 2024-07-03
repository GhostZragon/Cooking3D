using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ServiceInstaller<T> : MonoBehaviour , ServiceLocator.IGameService
{
    protected virtual void Register()
    {
        var type = typeof(T);
        ServiceLocator.Current.Register(GetService());
    }
    protected abstract ServiceLocator.IGameService GetService();
}

[DefaultExecutionOrder(-99)]
public class UIOrderManager : MonoBehaviour, ServiceLocator.IGameService
{
    //public static UIOrderManager instance;
    private UnityPool<UIOrderRecipe> UIOrderPool;
    private IObjectPool<TextMeshProUGUI> textPool;
    [SerializeField] private UIOrderRecipe uIOrderRecipePrefab;
    [SerializeField] private TextMeshProUGUI foodNeedTxtPrefab;
    [SerializeField] private Transform holder;
    [SerializeField] private Transform textHolderPool;
    [SerializeField] private Transform uiHolderPool;
  

    private void Awake()
    {
        textPool = new ObjectPool<TextMeshProUGUI>(CreateText, OnGet, OnRelease, DestroyText, true, 10);
        UIOrderPool = new UnityPool<UIOrderRecipe>(uIOrderRecipePrefab, 5, holder);
        ServiceLocator.Current.Register(this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Current.Unregister(this);
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
