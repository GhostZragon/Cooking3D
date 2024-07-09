using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavigationAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject targetg;
    [Button]
    private void Test()
    {
        agent.SetDestination(targetg.transform.position);
    }
}
