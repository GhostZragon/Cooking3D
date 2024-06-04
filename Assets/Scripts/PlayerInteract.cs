using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    public Transform PlayerItem;
    public Transform Container;
    public Vector3 size;
    public Transform interactTransform;

    [SerializeField] private float timer = 0;
    

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (interactTransform.position - transform.position).normalized, out hit, 5))
        {
            if (hit.collider.TryGetComponent(out SourceContainer sourceContainer))
            {
                GetInfinityItem(sourceContainer);
                return;
            }

            if(hit.collider.TryGetComponent(out Container container))
            {
                GetItemFromContainer(container);
            }

        }
    }

    private void GetInfinityItem(SourceContainer sourceContainer)
    {
        if (PlayerItem != null)
        {
            Debug.Log("You allready have item");
            return;
        }
        PlayerItem = sourceContainer.RetrieveRawFood().transform;
        SetItemParent(PlayerItem, Container);
    }

    private void GetItemFromContainer(Container container)
    {
        if (container == null)
        {
            Debug.Log("this null, pls check it", gameObject);
            return;
        }
        Debug.Log(container.name);

        var currentItem = PlayerItem;
        var containerItem = container.Item;

        // Swap
        container.Item = currentItem;
        PlayerItem = containerItem;
        Debug.Log("Container item: "+ container.Item, container.gameObject);
        // Set parent
        SetItemParent(PlayerItem, Container);

        SetItemParent(container.Item, container.PlaceTransform);

    }
    private void SetItemParent(Transform item,Transform parent)
    {
        if (item == null) return;
        item.parent = parent;
        item.localPosition = Vector3.zero;
    }
   
}
