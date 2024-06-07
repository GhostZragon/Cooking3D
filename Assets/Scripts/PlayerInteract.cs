using EasyButtons;
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
            if (hit.collider.TryGetComponent(out SourceFoodContainer sourceFoodContainer))
            {
                if (food != null) return;
                var sourceFood = sourceFoodContainer.RetrieveRawFood();
                this.food = sourceFood;
                food.SetToParentAndPosition(placeTransform);
                return;
            }
            else if (hit.collider.TryGetComponent(out SourcePlateContainer sourcePlateContainer))
            {
                if (plate != null) return;
                var sourcePlate = sourcePlateContainer.RetrieveRawFood();
                this.plate = sourcePlate;
                plate.SetToParentAndPosition(placeTransform);
                return;
            }

            // if (hit.collider.TryGetComponent(out HolderAbstract holder))
            // {
            //     holder.ExchangeItems(this);
            // }
            if (hit.collider.TryGetComponent(out IHolder holder))
            {
                holder.ExchangeItems(this);
            }


            // if (hit.collider.TryGetComponent(out SourceFoodContainer sourceContainer))
            // {
            //     // get infinity item if player not have item in hand
            //     if (PlayerItem != null)
            //     {
            //         Debug.Log("You allready have item");
            //         return;
            //     }
            //
            //     PlayerItem = sourceContainer.RetrieveRawFood();
            //     UpdateFoodParent(PlayerItem, Container);
            //     return;
            // }

            // 1. container have item and player have item (put item in item)
            // 2. container have item and player not have item (swap both of this)
            // 3. container have item and item not contain item
            // USE FOR NULL AND NOT NULL PLAYER ITEM
            // if (hit.collider.TryGetComponent(out Container container))
            // {
            //     // swap plate and food (custom)
            //
            //
            //     if (container.IsContainPlate() || PlayerIsContainPlate())
            //     {
            //         bool isContainFood = PlayerItem != null || container.Item != null;
            //
            //         if (isContainFood)
            //         {
            //             PutFoodIntoPlate(container);
            //         }
            //         else
            //         {
            //             SwapPlate(container);
            //         }
            //     }
            //     else // 2. player have item and collider is container, 
            //     {
            //         // Work with:
            //         // 1. Player have item, container have item
            //         // 2. Player not have item, container have item
            //         // 3. Player have item, container not have item
            //         TradeFood(container);
            //     }
            // }
        }
    }

    // private void SwapPlate(Container container)
    // {
    //     Debug.Log("Bat dau swap dia");
    //     var tempPlayerPlate = playerPlate;
    //     var tempContainerPlate = container.plate;
    //
    //     playerPlate = tempContainerPlate;
    //     container.plate = tempPlayerPlate;
    //
    //     UpdatePlateParent(playerPlate, Container);
    //     UpdatePlateParent(container.plate, container.PlaceTransform);
    // }
    //
    // private void PutFoodIntoPlate(Container container)
    // {
    //     Plate plate = playerPlate != null ? playerPlate : container.plate;
    //     Food food = PlayerItem != null ? PlayerItem : container.Item;
    //
    //     PlayerItem = null;
    //     container.Item = null;
    //     // swap plate and food logic
    //
    //     // if (SwapPlateAndFood(food, plate, container)) return;
    //
    //     plate.Add(food);
    //     UpdateFoodParent(food, plate.PlaceTransform);
    // }
    //
    //
    // /// <summary>
    // /// ExchangeItems food of player and food of container
    // /// </summary>
    // /// <param name="container"></param>
    // private void TradeFood(Container container)
    // {
    //     Debug.Log(container.name);
    //
    //     var currentItem = PlayerItem;
    //     var containerItem = container.Item;
    //
    //     // ExchangeItems
    //     container.Item = currentItem;
    //     PlayerItem = containerItem;
    //     Debug.Log("Container item: " + container.Item, container.gameObject);
    //     // Set parent
    //     UpdateFoodParent(PlayerItem, Container);
    //
    //     UpdateFoodParent(container.Item, container.PlaceTransform);
    // }
    //
    // private void UpdateFoodParent(Food item, Transform parent)
    // {
    //     if (item == null) return;
    //     SetItemParent(item.transform, parent);
    // }
    //
    // private void UpdatePlateParent(Plate item, Transform parent)
    // {
    //     if (item == null) return;
    //     SetItemParent(item.transform, parent);
    // }
    //
    // private void SetItemParent(Transform item, Transform parent)
    // {
    //     if (item == null) return;
    //     item.parent = parent;
    //     item.localPosition = Vector3.zero;
    // }
    // private bool SwapPlateAndFood(Food food, Plate plate, Container container)
    // {
    //     if (swapFoodAndPlate)
    //     {
    //         var foodParent = food.transform.parent;
    //         var plateParent = plate.transform.parent;
    //
    //         if (foodParent == transform)
    //         {
    //             PlayerItem = null;
    //             container.Item = food;
    //         }
    //         else
    //         {
    //             PlayerItem = food;
    //             container.Item = null;
    //         }
    //
    //         if (plateParent == transform)
    //         {
    //             playerPlate = null;
    //             container.plate = plate;
    //         }
    //         else
    //         {
    //             playerPlate = plate;
    //             container.plate = null;
    //         }
    //
    //
    //         UpdateFoodParent(food, plateParent);
    //         UpdatePlateParent(plate, foodParent);
    //
    //         return true;
    //     }
    //
    //     return false;
    // }
}