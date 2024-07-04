using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreHandle : ServiceInstaller<UIScoreHandle>, ServiceLocator.IGameService
{
    private Image coinImage;
    private TextMeshProUGUI coinText;
    private bool isCall = false;
    protected override void CustomAwake()
    {
        base.CustomAwake();
        coinImage = GetComponentInChildren<Image>();
        coinText = GetComponentInChildren<TextMeshProUGUI>();
    }
    [Button]
    private void Test()
    {
        UpdateCoinText(0);
    }
    public void UpdateCoinText(int value)
    {
        coinText.transform.DOKill();
        coinText.transform.DOScale(Vector2.one * 1.2f, .25f).SetLoops(2, LoopType.Yoyo);
        coinText.transform.DOShakePosition(2f, new Vector3(10, 0, 0), 10, 90, false, true).OnComplete(() =>
        {
            coinText.transform.DOScale(Vector2.one, .25f);
        });
        coinText.text = value.ToString();
    }
}
