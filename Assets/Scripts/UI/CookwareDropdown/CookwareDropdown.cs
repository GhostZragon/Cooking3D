using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CookwareDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown Dropdown;
    private List<string> dropdowns = new List<string>{"None","Pot","Pan","Oven"};
    private void Start()
    {
        dropdowns = new List<string>()
        {
            CookwareType.None.ToString(),
            CookwareType.Plate.ToString(),
            CookwareType.Pot.ToString(),
            CookwareType.Pan.ToString()
        };


        Dropdown.ClearOptions();
        Dropdown.AddOptions(dropdowns);
        
    }
    public void AddListener(UnityAction<int> action)
    {
        Dropdown.onValueChanged.AddListener(action);
    }

    internal void SetCookwareType(int cookwareTypeIndex)
    {
        Dropdown.value = cookwareTypeIndex;
        Dropdown.RefreshShownValue();
    }
}
