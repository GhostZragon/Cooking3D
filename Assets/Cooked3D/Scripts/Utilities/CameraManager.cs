using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera gameplayCamera;
    public CinemachineVirtualCamera startGameCamera;
    public Action callback;
    private void Awake()
    {
        ChangeStartCamera();
    }
    private void Start()
    {
        //Invoke(nameof(ChangeToGameplayCamera),1);
    }
    [Button]
    public void ChangeToGameplayCamera()
    {
        gameplayCamera.Priority = 20;
        startGameCamera.Priority = 10;
    }
    [Button]
    public void ChangeStartCamera()
    {
        gameplayCamera.Priority = 10;
        startGameCamera.Priority = 20;
    }

    public void OnTranstionCallBack()
    {
        StopCoroutine(StartCameraChangeCallback());
        StartCoroutine(StartCameraChangeCallback());
    }
    private IEnumerator StartCameraChangeCallback()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Callback");
        callback?.Invoke();
    }
    
}
public class CameraState : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject UIToShow;

    public void Show()
    {
        virtualCamera.Priority = 20;
        StopCoroutine(StartCameraChangeCallback());
        StartCoroutine(StartCameraChangeCallback());
    }
    private void Hide()
    {
        virtualCamera.Priority = 10;
        StopCoroutine(StartCameraChangeCallback());
        StartCoroutine(StartCameraChangeCallback());
    }
    private IEnumerator StartCameraChangeCallback()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Callback");
    }
}