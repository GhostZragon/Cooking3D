using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> OnMove;
    public static event Action OnInteract;
    private Vector3 moveDirection;
    public KeyCode keyCodeInteract;
    public void Update()
    {
        OnMoveInput();
        if (Input.GetKeyDown(keyCodeInteract))
        {
            OnInteract?.Invoke();
        }
    }

    private void OnMoveInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");

        if (moveDirection != Vector3.zero)
        {
            OnMove?.Invoke(moveDirection);
        }
    }
}
