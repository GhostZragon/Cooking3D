using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputReader input;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        input.EnableInput();
    }


    private void Update()
    {
        Move(input.Direction);
    }

    private void Move(Vector2 movementVector)
    {
        playerMovement.Move(new Vector3(movementVector.x,0,movementVector.y));
    }
}
