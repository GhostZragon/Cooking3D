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

    public Color colorPointerEnter;
    public Color colorPointerExit;


    private void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        var root = UIDocument.rootVisualElement;
        playButton = root.Q<Button>("Button_Play");
        optionsButton = root.Q<Button>("Button_Options");
        exitButton = root.Q<Button>("Button_Exit");

        //InitializeButtonEffects();
        colorPointerExit = playButton.style.backgroundColor.value;
    }

    private void InitializeButtonEffects()
    {
        AddButtonEffect(playButton);
        AddButtonEffect(optionsButton);
        AddButtonEffect(exitButton);
    }

    private void AddButtonEffect(Button button)
    {
        button.RegisterCallback<PointerEnterEvent>(OnButtonPointerEnter);
        button.RegisterCallback<PointerOutEvent>(OnButtonPointerExit);
    }
    private bool onRotate = false;
    private void OnButtonPointerEnter(PointerEnterEvent evt)
    {
        if (evt.target is Button button)
        {
            var value = onRotate ? 3f : -3f;
            button.style.backgroundColor = colorPointerEnter;
            button.style.rotate = new StyleRotate(new Rotate(value));
            button.style.scale = new StyleScale(new Scale(Vector2.one * 1.1f));
            onRotate = !onRotate;
        }
    }
    private void OnButtonPointerExit(PointerOutEvent evt)
    {
        if (evt.target is Button button)
        {
            button.style.backgroundColor = colorPointerExit;
            button.style.rotate = new StyleRotate(new Rotate(0f));
            button.style.scale = new StyleScale(new Scale(Vector2.one * 1));
        }
    }
}
