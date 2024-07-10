using StatefulUISupport.Scripts.Components;
using UnityEngine;
using UnityEngine.UI;

public class UIStartGameButton : StatefulView
{
    public Button playButton;
    public Button exitButton;

    private void Awake()
    {
        playButton = GetButton(ButtonRole.Play);
        exitButton = GetButton(ButtonRole.Exit);
    }
}