using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-150)]
public class GameManager : MonoBehaviour
{
    public int score = 0;
    private Camera mainCam;
    GameTimer gameTimer;
    public static Action<ScoreGrade, Vector3> AddScore;
    protected void Awake()
    {
        ServiceLocator.Initiailze();
        mainCam = Camera.main;
    }
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
                value = 50;
                popupColor = Color.yellow;
                break;
            case ScoreGrade.Medium:
                value = 100;
                popupColor = Color.green;
                break;
            case ScoreGrade.High:
                value = 150;
                popupColor = Color.blue;
                break;
            default:
                break;
        }

        score += value;
        ServiceLocator.Current.Get<UIScoreHandle>().UpdateCoinText(score);
        // Trigger the score popup with the appropriate color
        UITextPopupHandle.ShowTextAction?.Invoke(popupPosition, $"+{value}", popupColor);
    }

    public void StartGame()
    {
        GameTimer.StartCounter?.Invoke();
    }
}
public enum ScoreGrade
{
    None,
    Low,
    Medium,
    High
}
