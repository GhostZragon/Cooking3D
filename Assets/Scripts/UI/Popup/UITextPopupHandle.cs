using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextPopupHandle : MonoBehaviour
{
    public UITextPopup UITextPopupPrefab;
    public static Action<Vector3,string, Color> ShowTextAction;
    UITextPool _UITextPopupPool;
    private void Awake()
    {
        _UITextPopupPool = new UITextPool(UITextPopupPrefab, 5,transform);
    }


    private void OnEnable()
    {
        ShowTextAction += ShowText;
    }

    private void OnDisable()
    {
        ShowTextAction -= ShowText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowText(transform.position, "Test", Color.red);
        }
    }

    private void ShowText(Vector3 position,string text,Color color)
    {
        UITextPopup UITextPopup = _UITextPopupPool.Get();
        UITextPopup.OnCallback = (popup) =>
        {
            _UITextPopupPool.Release(popup);
        };
        Debug.Log("Position: "+position);
        UITextPopup.transform.localPosition = position;
        UITextPopup.SetText(text, color);
        UITextPopup.DoLocalAnimation();
        UITextPopup.SetStandPosition(position);
    }

}