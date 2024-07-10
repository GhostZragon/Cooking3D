using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static Action<ScoreGrade, Vector3> AddScore;
    public static Action<int> OnChangeScore;
    public int score;
    private void OnEnable()
    {
        AddScore += _AddScore;
    }
    private void OnDisable()
    {
        AddScore -= _AddScore;
    }
    private void _AddScore(ScoreGrade scoreGrade, Vector3 popupPosition)
    {
        var value = 0;
        Color popupColor = Color.white;

        switch (scoreGrade)
        {
            case ScoreGrade.None:
                value = 0;
                popupColor = Color.gray;
                break;
            case ScoreGrade.Low:
                value = 2;
                popupColor = Color.yellow;
                break;
            case ScoreGrade.Medium:
                value = 3;
                popupColor = Color.green;
                break;
            case ScoreGrade.High:
                value = 5;
                popupColor = Color.blue;
                break;
            default:
                break;
        }

        score += value;
        OnChangeScore?.Invoke(score);
        // Trigger the score popup with the appropriate color
        UITextPopupHandle.ShowTextAction?.Invoke(popupPosition, $"+{value}", popupColor);
    }
}