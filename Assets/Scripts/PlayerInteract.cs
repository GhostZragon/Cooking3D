using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    public Food PlayerItem;
    public Transform Container;
    [FormerlySerializedAs("plate")] public Plate playerPlate;
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

    private bool PlayerIsContainPlate()
    {
        return playerPlate != null;
    }
    [Button]
    private void OnInteract()
    {
        if (timer < 0.1f) return;
        timer = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (interactTransform.position - transform.position).normalized, out hit,
                5))
        {
            if (hit.collider.TryGetComponent(out SourceContainer sourceContainer))
            {
                // get infinity item if player not have item in hand
                if (PlayerItem != null)
                {
                    Debug.Log("You allready have item");
                    return;
                }

                GetInfinityItem(sourceContainer);
                return;
            }

            // 1. container have plate and player have item (put item in plate)
            // 2. container have plate and player not have item (swap both of this)
            // 3. container have plate and plate not contain item
            // USE FOR NULL AND NOT NULL PLAYER ITEM
            if (hit.collider.TryGetComponent(out Container container))
            {
                if (container.IsContainPlate() || PlayerIsContainPlate())
                {
                    var _playerPlate = playerPlate;
                    var containerPlate = container.plate;

                    playerPlate = containerPlate;
                    container.plate = playerPlate;

                    SetItemParent(playerPlate, Container);
                    SetItemParent(container.plate,container.PlaceTransform);

                }
                else // 2. player have plate and collider is container, 
                {
                    // Work with:
                    // 1. Player have item, container have item
                    // 2. Player not have item, container have item
                    // 3. Player have item, container not have item
                    GetItemFromContainer(container);
                }
            }
        }
    }

    private void GetInfinityItem(SourceContainer sourceContainer)
    {
        PlayerItem = sourceContainer.RetrieveRawFood();
        SetItemParent(PlayerItem, Container);
    }

    private void GetItemFromContainer(Container container)
    {
        Debug.Log(container.name);

        var currentItem = PlayerItem;
        var containerItem = container.Item;

        // Swap
        container.Item = currentItem;
        PlayerItem = containerItem;
        Debug.Log("Container item: " + container.Item, container.gameObject);
        // Set parent
        SetItemParent(PlayerItem, Container);

        SetItemParent(container.Item, container.PlaceTransform);
    }

    private void SetItemParent(Food item, Transform parent)
    {
        if (item == null) return;
        item.transform.parent = parent;
        item.transform.localPosition = Vector3.zero;
    }
    private void SetItemParent(Transform item, Transform parent)
    {
        if (item == null) return;
        item.parent = parent;
        item.localPosition = Vector3.zero;
    }
    private void SetItemParent(Plate plate, Transform parent)
    {
        if (plate == null) return;
        plate.transform.parent = parent;
        plate.transform.localPosition = Vector3.zero;
    }
}