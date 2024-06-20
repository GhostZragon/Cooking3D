using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class UITextPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private float yPos = 80;
    private bool isAnimated = false;
    [SerializeField] private UIHoldWorldPosition UIHoldWorldPosition;
    private void OnEnable()
    {
        isAnimated = false;
    }

    private void Awake()
    {
        TextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetText(string text,Color color)
    {
        TextMeshProUGUI.text = text;
        TextMeshProUGUI.color = color;
    }
    [Button]
    public void DoAnimation()
    {
        if (isAnimated)
        {
            return;
        }

        isAnimated = true;
        TextMeshProUGUI.transform.DOLocalMoveY(80, 1).OnComplete(() =>
        {
            Destroy(gameObject);
        }).SetEase(Ease.Linear);
    }

    public void SetStandPosition(Vector3 worldPosition)
    {
        UIHoldWorldPosition.SetStandPosition(worldPosition);
    }
}
