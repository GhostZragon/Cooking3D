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
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        btn = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ScaleTextUp()
    {
        text.transform.DOKill();
        text.transform.DOScale(Vector3.one * 1.25f,.1f);
    }
    public void ScalleTextDown()
    {
        text.transform.DOKill();
        text.transform.DOScale(Vector3.one, .1f);
    }
}