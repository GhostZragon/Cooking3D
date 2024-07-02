using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UIOrderManager : MonoBehaviour
{
    public static UIOrderManager instance;
    private UnityPool<UIOrderRecipe> UIOrderPool;
    public IObjectPool<TextMeshProUGUI> textPool;
    [SerializeField] private UIOrderRecipe uIOrderRecipePrefab;
    [SerializeField] private TextMeshProUGUI foodNeedTxtPrefab;
    [SerializeField] private Transform holder;
    private void Awake()
    {
        instance = this;
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
        text.transform.SetParent(transform, false);
    }
    private void DestroyText(TextMeshProUGUI text)
    {
        Destroy(text.gameObject);
    }
}
