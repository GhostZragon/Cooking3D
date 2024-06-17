using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    [SerializeField] private Transform centerTransform;
    [SerializeField] private HolderAbstract playerHolder;
    [SerializeField] private float InteractionCooldown = .1f;

    [Header("Raycast settings")] [SerializeField]
    private LayerMask interactMask;

    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private Color rayColor = Color.red;
    [SerializeField] private ContainerSelect currentContainer;
    public float yOffset;
    private Vector3 ForwardDirection;
    private Vector3 LeftDirection;
    private Vector3 RightDirection;
    private Vector3 RayOrigin;

    private void Awake()
    {
        InputManager.OnInteract += HandleInteract;
    }

    private void OnDestroy()
    {
        InputManager.OnInteract -= HandleInteract;
    }

    private void Update()
    {
        UpdateTimer();
    }


    private void FixedUpdate()
    {
        RefreshDirection();
        DrawRay();
    }

    private void RefreshDirection()
    {
        if (centerTransform == null) return;
        ForwardDirection = transform.forward + new Vector3(0, yOffset, 0);
        LeftDirection = -transform.right + new Vector3(0, yOffset, 0);
        RightDirection = transform.right + new Vector3(0, yOffset, 0);
        RayOrigin = centerTransform.position;

        Debug.DrawRay(RayOrigin, ForwardDirection * rayDistance, rayColor);
        Debug.DrawRay(RayOrigin, LeftDirection * rayDistance, rayColor);
        Debug.DrawRay(RayOrigin, RightDirection * rayDistance, rayColor);
    }

    private void DrawRay()
    {
        if (GetRay(ForwardDirection) || GetRay(RightDirection) || GetRay(LeftDirection))
        {
        }
    }

    private bool GetRay(Vector3 to)
    {
        Ray ray = new Ray(RayOrigin, to);
        if (Physics.Raycast(RayOrigin, to, out var hitInfo, rayDistance, interactMask))
        {
            // Debug.Log(hitInfo.collider, hitInfo.collider.gameObject);
            if (hitInfo.collider.TryGetComponent(out ContainerSelect containerSelect) && containerSelect != currentContainer)
            {
                if (currentContainer == null)
                {
                    currentContainer = containerSelect;
                    currentContainer.SetSelect();
                }
                else if (currentContainer != null && currentContainer != containerSelect)
                {
                    currentContainer.SetNormal();
                    currentContainer = containerSelect;
                    currentContainer.SetSelect();
                }
            }
            return true;
        }
        else
        {
            if (currentContainer != null)
                currentContainer.SetNormal();
            currentContainer = null;
        }

        return false;
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer >= InteractionCooldown) timer = InteractionCooldown;
    }

    [Button]
    private void HandleInteract()
    {
        if (timer < 0.1f) return;
        timer = 0;
        Debug.Log("Interact");
        if (currentContainer == null) return;
        if (currentContainer.TryGetComponent(out IHolder iholder))
        {
            Debug.Log("Contain I holder interface", currentContainer.gameObject);
        }
    }
}