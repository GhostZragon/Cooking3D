using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    public Transform Item;
    public Transform ParentItemTransform;
    public Vector3 size;
    public Transform interactTransform;

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
        Debug.DrawRay(transform.position, (interactTransform.position - transform.position).normalized, Color.red);
    }
    [Button]
    private void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (interactTransform.position - transform.position).normalized, out hit, 5))
        {
            if(hit.collider.TryGetComponent(out Container container))
            {
                GetItemFromContainer(container);
            }
        }
    }
    private void GetItemFromContainer(Container container)
    {
        if (container == null)
        {
            Debug.Log("this null, pls check it", gameObject);
            return;
        }

        if (Item == null)
        {
            // pickup
            Debug.Log("On change");
            var _Item = container.TryGetItem();
            if(_Item != null)
            {
                Item = _Item;
                Item.transform.SetParent(ParentItemTransform);
                Item.localPosition = Vector3.zero;
                Item.transform.position = ParentItemTransform.position;
            }
           
        }
        else
        {
            Debug.Log("On Drop");
            container.SetItemInPlace(Item);
            Item = null;
        }
    }
}
