using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5;
    public float rotateSpeed = 3;
    public Vector3 direction;
    public float angle = 0;
    public int obstacleLayerMask;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        InputManager.OnMove += Move;
    }
    private void OnDestroy()
    {
        InputManager.OnMove -= Move;
    }
    private void Move(Vector3 direction)
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        controller.Move(direction.normalized * speed * Time.deltaTime);
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        transform.forward = direction;
    }
}
