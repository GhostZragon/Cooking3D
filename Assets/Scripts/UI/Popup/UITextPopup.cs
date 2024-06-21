using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public interface PoolCallback<T>
{
    Action<T> OnCallback { get; set; }
}
public class UITextPopup : MonoBehaviour,PoolCallback<UITextPopup>
{
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private float yPos = 80;
    [SerializeField] private UIHoldWorldPosition UIHoldWorldPosition;
    private bool isAnimated = false;
    private void OnEnable()
    {
        isAnimated = false;
        TextMeshProUGUI.transform.localPosition = Vector3.zero;
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
    public void DoLocalAnimation()
    {
        if (isAnimated)
        {
            return;
        }

        isAnimated = true;
        TextMeshProUGUI.transform.DOLocalMoveY(80, 1).OnComplete(() =>
        {
            OnCallback?.Invoke(this);
        }).SetEase(Ease.Linear);
    }

    public void SetStandPosition(Vector3 worldPosition)
    {
        UIHoldWorldPosition.SetStandPosition(worldPosition);
    }

    public Action<UITextPopup> OnCallback { get; set; }
}
