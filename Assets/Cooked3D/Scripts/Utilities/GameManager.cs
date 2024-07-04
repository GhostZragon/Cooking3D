using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-150)]
public class GameManager : ServiceInstaller<GameManager>,ServiceLocator.IGameService
{
    public int score = 0;
    private Camera mainCam;
    GameTimer gameTimer;
    protected override void CustomAwake()
    {
        base.CustomAwake();
        ServiceLocator.Initiailze();
        mainCam = Camera.main;
    }


    public void AddScore(ScoreGrade scoreGrade, Vector3 popupPosition)
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
        popupPosition = mainCam.WorldToScreenPoint(popupPosition);
        // Trigger the score popup with the appropriate color
        UITextPopupHandle.ShowTextAction?.Invoke(popupPosition, $"+{value}", popupColor);
    }
}
public enum ScoreGrade
{
    None,
    Low,
    Medium,
    High
}
