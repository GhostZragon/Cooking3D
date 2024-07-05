using System;

public interface PoolCallback<T>
{
    Action<T> OnCallback { get; set; }
    void OnRelease();
}
