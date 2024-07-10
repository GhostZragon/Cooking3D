using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StatefulUI.Runtime.Core;
using StatefulUISupport.Scripts.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIYesNoPopup : StatefulView
{
    public TextMeshProUGUI yesNoHeaderText;
    public Button yesButton;
    public Button noButton;

    public Action YesCallback;
    public Action NoCallback;
    private void Awake()
    {
        yesNoHeaderText = GetText(TextRole.YesNoHeader).TMP;
        yesButton = GetButton(ButtonRole.Yes);
        noButton = GetButton(ButtonRole.No);

        yesButton.onClick.AddListener(OnYesClick);
        noButton.onClick.AddListener(OnNoClick);
    }

    public void Show(string header)
    {
        gameObject.SetActive(true);
        //yesButton.transform.DOKill();
        //noButton.transform.DOKill();
        //yesButton.transform.DOScale(Vector3.one, .15f);
        //noButton.transform.DOScale(Vector3.one, .15f);

        yesNoHeaderText.text = header;
    }
    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(OnYesClick);
        noButton.onClick.RemoveListener(OnNoClick);
    }


    private void OnYesClick()
    {
        StartCoroutine(OnHide(YesCallback));
    }

    private void OnNoClick()
    {
        StartCoroutine(OnHide(NoCallback));
    }

    private IEnumerator OnHide(Action callback)
    {
  
        callback?.Invoke();
        gameObject.SetActive(false);
        yield return null;
    }
}
