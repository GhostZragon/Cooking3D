using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5;
    [SerializeField] private Rigidbody rb;
    private Animator animator;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    public void Move(Vector3 direction)
    {
   
        if(direction == Vector3.zero)
        {
            animator.ResetTrigger("Moving");
        }
        else
        {
            animator.SetTrigger("Moving");
        }

        Vector3 move = direction.normalized * speed * Time.fixedDeltaTime;
        controller.Move(move);
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
