using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        InputManager.OnMove += Move;
    }
    private void OnDestroy()
    {
        InputManager.OnMove -= Move;
    }

    private void Move(Vector3 vector)
    {
        playerMovement.Move(vector);
    }
}
