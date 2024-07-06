using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void SetDestination(Transform target)
    {
        this.target = target;
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (target == null) return;
        if (agent.remainingDistance > 0) return;
    }
}