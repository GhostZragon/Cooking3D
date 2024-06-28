using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    public void Move(Vector3 direction)
    {
        if (controller == null)
        {
            Debug.LogWarning("CharacterController is null", gameObject);
            return;
        }

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
