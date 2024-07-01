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
    public UIFoodProcessBar GetUIElement(Vector3 position)
    {
        return GetFromPool(position);
    }
}
public class UIWorldSpaceItem<T> : MonoBehaviour where T : UIWorldSpace, PoolCallback<T>
{
    public UnityPool<T> pool;
    public T uiItemPrefab;
    public int size = 5;

    protected virtual void Awake()
    {
        pool = new UnityPool<T>(uiItemPrefab, size, transform);
    }
    
    protected virtual T GetFromPool(Vector3 position)
    {
        var worldSpaceUIElement = pool.Get();
        worldSpaceUIElement.SetStandPosition(position);
        return worldSpaceUIElement;
    }
}