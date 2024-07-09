using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStartMenu : MonoBehaviour
{
    private UIDocument UIDocument;
    private Button playButton;
    private Button optionsButton;
    private Button exitButton;


    public bool showUI = false;
    private void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        var root = UIDocument.rootVisualElement;
        playButton = root.Q<Button>("Button_Play");
        optionsButton = root.Q<Button>("Button_Options");
        exitButton = root.Q<Button>("Button_Exit");

        //InitializeButtonEffects();

        playButton.clicked += PlayButton_clicked;

        exitButton.clicked += ExitButton_clicked;
        Show(true);
    }

    private void ExitButton_clicked()
    {
        Application.Quit();
    }

    public void Show(bool enable)
    {
        UIDocument.enabled = enable;
    }
    private void PlayButton_clicked()
    {
        Show(false);
        EventManager.GameLoop.StartGame?.Invoke();
    }

}
public static class EventManager
{
    public static class GameLoop
    {
        public static Action StartGame;
        public static Action EndGame;
        public static Action ResetGame;
    }
}