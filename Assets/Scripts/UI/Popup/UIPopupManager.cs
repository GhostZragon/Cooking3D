using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;


public class UIPopupManager : MonoBehaviour
{
    public PopupText textPopupPrefab;
    [SerializeField] private string test_Textpopup = "Test";
    [SerializeField] private Color color;
    [Button]
    private void Test()
    {
        ShowPopupText(test_Textpopup, Vector3.zero, color);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    public void ShowPopupText(string text, Vector3 position, Color color)
    {
        var uiText = Instantiate(textPopupPrefab,transform);
        uiText.transform.localPosition = Vector3.zero;
        uiText.SetText(text,color);
        uiText.DoAnimation();
    }
}
