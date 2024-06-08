using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerInteract : HolderAbstract
{
    // public Food PlayerItem;
    // public Transform Container;
    // [FormerlySerializedAs("item")] public Plate playerPlate;
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

    // private bool PlayerIsContainPlate()
    // {
    //     return playerPlate != null;
    // }

    public bool swapFoodAndPlate = true;

    [Button]
    private void OnInteract()
    {
        if (timer < 0.1f) return;
        timer = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (interactTransform.position - transform.position).normalized, out hit,
                5))
        {
            if (hit.collider.TryGetComponent(out IHolder holder))
            {
                holder.ExchangeItems(this);
            }
        }
    }

  
}