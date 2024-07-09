using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreHandle : MonoBehaviour,ServiceLocator.IGameService
{
    [SerializeField] private Image coinImage;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Color textScaleColor;
    
    private bool isCall = false;
    
    private void Awake()
    {
        coinText.text = "0";
    }
    
    [Button]
    private void Test()
    {
        UpdateCoinText(40000);
    }
    
    public void UpdateCoinText(int value)
    {
        coinText.transform.DOKill();
        coinText.DOColor(textScaleColor, .15f);
        coinText.transform.DOScale(Vector3.one * 1.3f,.1f).OnComplete(() =>
        {
            coinText.transform.DOScale(Vector2.one, .1f);
            coinText.DOColor(Color.white, .1f);
        }); ;

        coinText.text = value.ToString();
    }
}
