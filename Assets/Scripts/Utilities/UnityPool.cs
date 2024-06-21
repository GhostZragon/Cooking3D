using UnityEngine;
using UnityEngine.Pool;

public abstract class UnityPool<PoolObj> where PoolObj : MonoBehaviour
{
    protected IObjectPool<PoolObj> pool;
    protected PoolObj prefab;
    protected Transform parent;
    public abstract PoolObj Get();
    public UnityPool(PoolObj prefab, int size = 5,Transform parent = null)
    {
        this.parent = parent;
        this.prefab = prefab;
        pool = new ObjectPool<PoolObj>(OnCreate, OnGet, OnRelease, OnDestroy, true, size,20);
    }

    protected abstract void OnDestroy(PoolObj obj);

    protected abstract void OnRelease(PoolObj obj);

    protected abstract void OnGet(PoolObj obj);

    protected abstract PoolObj OnCreate();
    public void Release(PoolObj obj)
    {
        pool.Release(obj);
    }

    public void Clear()
    {
        pool.Clear();
    }
}