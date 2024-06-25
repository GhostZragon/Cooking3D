using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookwareDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown Dropdown;
    private List<string> dropdowns = new List<string>{"None","Pot","Pan","Oven"};
    private void Start()
    {
        Dropdown.ClearOptions();
        Dropdown.AddOptions(dropdowns);
    }
}
