using DG.Tweening;
using NaughtyAttributes;
using StatefulUISupport.Scripts.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIStartGameButton : StatefulView
{
    public CustomButton playButton;
    public CustomButton exitButton;
    public CustomButton optionsButton;
    private List<CustomButton> listButton;
    private void Awake()
    {
        playButton = GetButton(ButtonRole.Play).GetComponent<CustomButton>();
        optionsButton = GetButton(ButtonRole.Options).GetComponent<CustomButton>();
        exitButton = GetButton(ButtonRole.Exit).GetComponent<CustomButton>();
        listButton = GetComponentsInChildren<CustomButton>().ToList();

    }
    [Button]
    private void Show()
    {
        ScaleBtn(Vector3.one, .25f, .1f);
    }
    [Button]
    private void Hide()
    {
        ScaleBtn(Vector3.zero, .25f, .1f);
    }
    public void ScaleBtn(Vector3 scale, float scaleTime, float timeScalePerBtn = .1f)
    {
        StartCoroutine(_ScaleBtn(scale, scaleTime, timeScalePerBtn));
    }
    private IEnumerator _ScaleBtn(Vector3 scale, float scaleTime, float timeScalePerBtn = .1f)
    {
        var fadevalue = scale == Vector3.one ? 1 : 0;
        foreach(var item in listButton)
        {
            item.transform.DOScale(scale, scaleTime).SetEase(Ease.Linear);
            item.canvasGroup.DOFade(fadevalue, scaleTime / 2).SetEase(Ease.Linear);

            yield return new WaitForSeconds(timeScalePerBtn);
        }
    }

}
