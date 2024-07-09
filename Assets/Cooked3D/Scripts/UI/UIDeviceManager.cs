using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeviceManager : MonoBehaviour
{
    public GameObject MoblieUI;
    public bool IsMoblie = true;
    private void Awake()
    {
        MoblieUI.gameObject.SetActive(MoblieUI);
    }
}
