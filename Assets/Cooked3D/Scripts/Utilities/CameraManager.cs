using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera gameplayCamera;
    public CinemachineVirtualCamera startGameCamera;
    private void Awake()
    {
        ChangeStartCamera();
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
}
