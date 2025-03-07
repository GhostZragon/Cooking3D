using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextPopupHandle : UIWorldSpaceItem<UITextPopup>
{
    public static Action<Vector3,string, Color> ShowTextAction;


    private void InitPool()
    {
    }
    private void OnEnable()
    {
        ShowTextAction += ShowText;
    }

    private void OnDisable()
    {
        ShowTextAction -= ShowText;
    }

    private void ShowText(Vector3 position,string text,Color color)
    {
        UITextPopup UITextPopup = GetFromPool();
        UITextPopup.SetStandPosition(position);
        UITextPopup.transform.localPosition = position;
        UITextPopup.SetText(text, color);
        UITextPopup.DoLocalAnimation();
    }

}
