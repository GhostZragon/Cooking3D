using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    
    private void Awake()
    {
        ServiceLocator.Initiailze();

    }
    public static void Register<T>(T  type) where T : ServiceLocator.IGameService
    {
        ServiceLocator.Current.Register(type);
    }
    public static void UnRegister<T>(T type) where T : ServiceLocator.IGameService
    {
        ServiceLocator.Current.Unregister(type);
    }
}
