using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Customer CustomerPrefab;
    [SerializeField] private NPCsTable freeTable;
    private UnityPool<Customer> customerPool;
    private float timer;

    private IGameControl IGameControl;

    private void Awake()
    {
        customerPool = new UnityPool<Customer>(CustomerPrefab, 5, null);
        IGameControl = ServiceLocator.Current.Get<GameControl>();
    }
    private void Update()
    {
        if (IGameControl.canSpawnCustomer == false) return;

        if (freeTable.IsHaveTable)
        {
            if (timer < .5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                Spawner();
            }
            Debug.Log("On Spawn");

        }
    }

    [Button]
    private void Spawner()
    {
        timer = 0;
        var target = freeTable.GetEmptyChair();
        if (target == null) return;
        target.SetIsEmpty(false);

        var customer = customerPool.Get();
        customer.transform.position = transform.position;
        customer.SetExitPosition(transform.position);
        customer.SetDestination(target);
        customer.OnCreateOrderCallback = CreateOrder;
    }
    private void CreateOrder(Customer customer)
    {
        var order = ServiceLocator.Current.Get<RecipeOrderProcessor>().CreateOrder();
        if (order == null) return;
        order.CallbackCompleteOrder = (customer.GoOutSide);
    }
}
