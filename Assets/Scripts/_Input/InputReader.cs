using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "Input Reader",menuName = "3DGame/Input Reader")]
public class InputReader : ScriptableObject, PlayerInputAction.IPlayerActions
{
    public event UnityAction<Vector2> Move;
    public event UnityAction Interact;
    public event UnityAction DoAction; 

    private PlayerInputAction inputAction;
    public Vector3 Direction => inputAction.Player.Move.ReadValue<Vector2>();

    private void OnEnable()
    {
        if (inputAction == null)
        {
            inputAction = new PlayerInputAction();
            inputAction.Player.SetCallbacks(this);
        }
    }

    public void EnableInput()
    {
        inputAction.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Move?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Interact?.Invoke();
        }
    }

    public void OnDoAction(InputAction.CallbackContext context)
    {
        DoAction?.Invoke();
    }
}
