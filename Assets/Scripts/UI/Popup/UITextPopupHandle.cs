using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextPopupHandle : MonoBehaviour
{
    public UITextPopup UITextPopupPrefab;
    public static Action<Vector3,string, Color> ShowTextAction;

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
        var UITextPopup = Instantiate(UITextPopupPrefab, transform);
        UITextPopup.SetText(text, color);
        UITextPopup.DoAnimation();
        UITextPopup.SetStandPosition(position);
    }

}
