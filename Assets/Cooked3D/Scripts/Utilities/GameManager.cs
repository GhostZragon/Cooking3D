using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-150)]
public class CookedManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] GameControl gameControl;

    protected void Awake()
    {
        gameControl = new GameControl();
        gameControl.canSpawnFood = false;
        gameControl.canGetInput = false;
        gameControl.canSpawnCustomer = false;


        ServiceLocator.Initiailze();

        ServiceLocator.Current.Register(gameControl);

        cameraManager.callback += StartGame;

    }

    private void Update()
    {

    }
    private void StartGame()
    {
        gameControl.canSpawnFood = true;
        gameControl.canGetInput = true;
        gameControl.canSpawnCustomer = true;
    }

    private void OnDisable()
    {
        cameraManager.callback -= StartGame;
        ServiceLocator.Current.Unregister(gameControl);
    }
}
public class GameControl : IGameControl,ServiceLocator.IGameService
{
    public bool canSpawnFood { get; set; }
    public bool canSpawnCustomer { get; set; }
    public bool canGetInput { get; set; }
}
public enum ScoreGrade
{
    None,
    Low,
    Medium,
    High
}
public interface IGameControl
{
    bool canSpawnFood { get; set; }
    bool canSpawnCustomer { get; set; }
    bool canGetInput { get; set; }
}
