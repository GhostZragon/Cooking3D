using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(UIHoldWorldPosition))]
public class UIWorldSpace : MonoBehaviour
{
    protected UIHoldWorldPosition UIHoldWorldPosition;

    protected virtual void Awake()
    {
        UIHoldWorldPosition = GetComponent<UIHoldWorldPosition>();
    }

    public void SetStandPosition(Vector3 worldPosition)
    {
        if(UIHoldWorldPosition == null)
        {
            Debug.Log("null");
            return;
        }
        UIHoldWorldPosition.SetStandPosition(worldPosition);
    }

}
public class UITextPopup : UIWorldSpace, PoolCallback<UITextPopup>
{
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private float yPos = 80;
    private bool isAnimated = false;
    private void OnEnable()
    {
        isAnimated = false;
        TextMeshProUGUI.transform.localPosition = Vector3.zero;
    }
    private void OnDisable()
    {
        transform.DOKill();
        TextMeshProUGUI.transform.DOKill();
    }

    protected override void Awake()
    {
        base.Awake();
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

    public void OnRelease()
    {
        OnCallback?.Invoke(this);
    }

    public Action<UITextPopup> OnCallback { get; set; }
}
