using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    }

    private void PlayButton_clicked()
    {
    }

    private void PlayButton_onClick()
    {
    }
}
