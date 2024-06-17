using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    [SerializeField] private Transform interactTransform;
    [SerializeField] private HolderAbstract playerHolder;
    private void Awake()
    {
        InputManager.OnInteract += OnInteract;
    }

    private void OnDestroy()
    {
        InputManager.OnInteract -= OnInteract;
    }

    private void Update()
    {
        Timer();
        Debug.DrawRay(transform.position, (interactTransform.position - transform.position).normalized, Color.red);
        Debug.DrawRay(transform.position,Vector3.left,Color.green);
    }
    public float rayDistance = 5f; // Khoảng cách của ray
    public Color rayColor = Color.red; // Màu của ray khi vẽ bằng Debug
    private void OnDrawGizmos()
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 leftDirection = -transform.right;
        Vector3 rightDirection = transform.right;

        // Vị trí bắt đầu của ray
        Vector3 rayOrigin = transform.position;

        // Vẽ ray bằng Debug
        Debug.DrawRay(rayOrigin, forwardDirection * rayDistance, rayColor);
        Debug.DrawRay(rayOrigin, leftDirection * rayDistance, rayColor);
        Debug.DrawRay(rayOrigin, rightDirection * rayDistance, rayColor);
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= 0.1f) timer = .1f;
    }
    [Button]
    private void OnInteract()
    {
        if (timer < 0.1f) return;
        timer = 0;
        Debug.Log("Interact");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (interactTransform.position - transform.position).normalized, out hit,
                10))
        {
            if (hit.collider.TryGetComponent(out IHolder holder))
            {
                // Debug.Log(hit.collider.name);
                holder.ExchangeItems(playerHolder);
            }
        }
    }
}
