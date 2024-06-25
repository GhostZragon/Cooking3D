using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookwareManager : MonoBehaviour
{
    public CookwareData cookwareData;

    public Cookware cookwarePrefab;

    public Cookware GetCookware(CookwareType type)
    {
        var cookware = Instantiate(cookwarePrefab);
        cookware.SetCookwareType(type);
        return cookware;
    }
   
}
