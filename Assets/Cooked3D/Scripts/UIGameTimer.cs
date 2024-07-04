using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;
    private void Awake()
    {
        UpdateTimerText(0);
    }
    private void OnEnable()
    {
        GameTimer.OnUpdateTimer += UpdateTimerText;
    }

    private void OnDisable()
    {
        GameTimer.OnUpdateTimer -= UpdateTimerText;
    }

    private void UpdateTimerText(int timer)
    {
        int minutes = timer / 60;
        int seconds = timer % 60;
        textTimer.transform.DOShakeScale(.5f,.3f);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}