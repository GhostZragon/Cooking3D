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
    [Header("Raycast settings")]
    
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private float rayDistance = 5f; 
    [SerializeField] private Color rayColor = Color.red; 
    [SerializeField] private Collider containerCatch;
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
    }
  
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

    private void FixedUpdate()
    {
        FindCollider();

    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= 0.1f) timer = .1f;
    }

    private void FindCollider()
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 leftDirection = -transform.right;
        Vector3 rightDirection = transform.right;

        // Vị trí bắt đầu của ray
        Vector3 rayOrigin = transform.position;
        Ray ray1 = new Ray(rayOrigin, forwardDirection * rayDistance);
        if (Physics.Raycast(ray1,out var hitInfo,interactMask))
        {
            Debug.Log(hitInfo.collider);
        }
        // Ray ray2 = new Ray(rayOrigin, leftDirection * rayDistance);
        // Ray ray3 = new Ray(rayOrigin, rightDirection * rayDistance);
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
