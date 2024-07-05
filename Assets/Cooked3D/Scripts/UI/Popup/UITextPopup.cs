using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
public class UITextPopup : UIWorldSpace, PoolCallback<UITextPopup>
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private float yPos = 80;
    private bool isAnimated = false;
    private void OnEnable()
    {
        isAnimated = false;
        textUI.transform.localPosition = Vector3.zero;
    }
    private void OnDisable()
    {
        transform.DOKill();
        textUI.transform.DOKill();
    }

    protected override void Awake()
    {
        base.Awake();
        textUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetText(string text,Color color)
    {
        textUI.text = text;
        textUI.color = color;
    }
    [Button]
    public void DoLocalAnimation()
    {
        if (isAnimated)
        {
            return;
        }
        textUI.DOFade(1, 0);
        textUI.transform.DOPunchScale(Vector2.one, .25f);
        isAnimated = true;
        textUI.DOFade(0, 1);
        textUI.transform.DOLocalMoveY(80, 1).OnComplete(() =>
        {
            OnCallback?.Invoke(this);
        }).SetEase(Ease.Linear);
    }

    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }

    public Action<UITextPopup> OnCallback { get; set; }
}
