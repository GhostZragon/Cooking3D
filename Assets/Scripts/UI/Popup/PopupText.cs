using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
public class PopupText : MonoBehaviour
{
    public float yPos;
    private bool isAnimated = false;
    private TextMeshPro TextMeshProUGUI;

    private void Awake()
    {
        TextMeshProUGUI = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        isAnimated = false;
    }
    [Button]
    public void DoAnimation()
    {
        if (isAnimated) return;
        isAnimated = true;
        var targetPosY = transform.localPosition.y + yPos;
        transform.DOLocalMoveY(targetPosY, 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    public void SetText(string text, Color color)
    {
        TextMeshProUGUI.text = text;
        TextMeshProUGUI.color = color;
    }

}
