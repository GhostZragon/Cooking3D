using EasyButtons;
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
            if(hit.collider.TryGetComponent(out Container container))
            {
                if(container.GetType() == ContainerType.Container)
                    GetItemFromContainer(container);
                else
                {
                    GetItemFromSource(container);
                }
            }

        }
    }

    private void GetItemFromSource(Container component)
    {
      
    }

    private void GetItemFromContainer(Container container)
    {
        if (container == null)
        {
            Debug.Log("this null, pls check it", gameObject);
            return;
        }


        var currentItem = PlayerItem;
        var containerItem = container.Item;

        container.Item = currentItem;
        PlayerItem = containerItem;
    }

   
}
