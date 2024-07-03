using UnityEngine;

[DefaultExecutionOrder(-99)]
public abstract class ServiceInstaller<T> : MonoBehaviour where T : ServiceLocator.IGameService
{
    private T cachedRef;
    protected virtual void Awake()
    {
        Register();
        CustomAwake();
    }
    protected virtual void OnDestroy()
    {
        UnRegister();
        CustomOnDestroy();
    }
    protected virtual void CustomAwake()
    {

    }
    protected virtual void CustomOnDestroy()
    {

    }
    private void Register()
    {
        ServiceLocator.Current.Register(GetService());
    }
    private void UnRegister()
    {
        ServiceLocator.Current.Unregister(GetService());
    }
    
    protected virtual T GetService()
    {
        if(cachedRef == null)
        {
            cachedRef = (T)(object)this;
        }
        return cachedRef;
    }
}
