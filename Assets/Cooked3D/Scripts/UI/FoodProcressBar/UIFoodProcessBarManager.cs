using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIFoodProcessBarManager : UIWorldSpaceItem<UIFoodProcessBar>
{

    public static UIFoodProcessBarManager instance;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public UIFoodProcessBar GetUIElement()
    {
        return GetFromPool();
    }
}
