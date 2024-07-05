using UnityEngine;

public class UIWorldSpaceItem<T> : MonoBehaviour where T : UIWorldSpace, PoolCallback<T>
{
    public UnityPool<T> pool;
    public T uiItemPrefab;
    public int size = 5;

    protected virtual void Awake()
    {
        pool = new UnityPool<T>(uiItemPrefab, size, transform);
    }
    
    protected virtual T GetFromPool()
    {
        return pool.Get();
    }
}
