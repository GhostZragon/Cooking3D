using TMPro;
using UnityEngine;

public class UIGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;

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
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}