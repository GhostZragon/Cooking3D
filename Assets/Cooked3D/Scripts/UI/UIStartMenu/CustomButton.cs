using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class CustomButton : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI text;
    public Button btn;
    public Image icon;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        btn = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ScaleTextUp()
    {
        KillTween();
        //transform.DOScale(Vector3.one * 1.1f,.15f).SetEase(Ease.Linear);
        text.transform.DOScale(Vector3.one * 1.15f, .1f);
        icon.transform.gameObject.SetActive(true);
        icon.DOFade(1, .25f).SetEase(Ease.Linear);
        //icon.transform.DOShakeScale(.1f).SetEase(Ease.Linear).OnComplete(() =>
        //{
        //    icon.transform.localScale = Vector3.one;
        //});
    }
    public void ScalleTextDown()
    {
        KillTween();
        text.transform.DOScale(Vector3.one, .1f);
        icon.transform.gameObject.SetActive(false);
        icon.DOFade(0, .25f).SetEase(Ease.Linear);
        //transform.DOScale(Vector3.one, .15f).SetEase(Ease.Linear);

    }

    private void KillTween()
    {
        transform.DOKill();
        text.transform.DOKill();
        icon.transform.DOKill();
    }
}