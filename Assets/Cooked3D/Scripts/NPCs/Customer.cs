using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour, PoolCallback<Customer>
{
    [SerializeField] private float distance;
    [SerializeField] private float angle;
    [SerializeField] private Transform model;
    [SerializeField] private Vector3 exitPosition;
    
    private WaitUntil waitUntilGoToDestination;
    private NavMeshAgent agent;
    private NpcChair npcChair;
    private Animator animator;

    public Action<Customer> OnCreateOrderCallback;

    public Action<Customer> OnCallback { get; set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        Setup();

    }

    private void Setup()
    {
        waitUntilGoToDestination = new WaitUntil(() => agent.remainingDistance == 0);

    }
    public void SetDestination(NpcChair target)
    {
        // agent.isStopped = false;
        this.npcChair = target;
        agent.SetDestination(target.GetSitPosition());
        StartCoroutine(GoToSitDownPlace());
    }
    
    private IEnumerator GoToSitDownPlace()
    {
        MAnimator.HarshPlay(animator, NPCAnimator.Walking);
        Debug.Log("wait");

        yield return new WaitForSeconds(.1f);
        yield return new WaitUntil(() => agent.remainingDistance == 0);
        // agent.isStopped = true;
        MAnimator.HarshPlay(animator, NPCAnimator.SitChairDown);
        MAnimator.WaitPlay(animator, NPCAnimator.SitChairIdle,.2f);
        
        var direction = npcChair.GetTableTransform() - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3Extensions.NormalizeDirectionToCardinal(direction));
 
        yield return new WaitForSeconds(1);
        
        OnCreateOrderCallback?.Invoke(this);

    }
    private void Update()
    {
        distance = agent.remainingDistance;
    }
    public void GoOutSide() => StartCoroutine(GoOutside());
    private IEnumerator GoOutside()
    {
        Debug.Log("Done bussines");
        // agent.isStopped = false;
  
        yield return MAnimator.PlayAndWait(animator, NPCAnimator.SitChairStandUp, .2f);
        agent.SetDestination(exitPosition);
        Debug.Log("Get out");
        MAnimator.HarshPlay(animator,NPCAnimator.Walking);
        Debug.Log("Wait animation done");
        // agent.isStopped = true;
        yield return new WaitForSeconds(.1f);
        yield return new WaitUntil(() => agent.remainingDistance == 0);
        
        GetOut();
        agent.isStopped = true;
        OnCallback?.Invoke(this);
        Debug.Log("return to pool");
    }


    private void GetOut()
    {
        npcChair.SetIsEmpty(true);
        npcChair = null;
        OnCreateOrderCallback = null;
    }

    public void SetExitPosition(Vector3 exitPosition)
    {
        this.exitPosition = exitPosition;
    }

    public void OnRelease()
    {

    }
}

public static class Vector3Extensions
{

    public static Vector3 NormalizeDirectionToCardinal(Vector3 direction)
    {
        // Determine the primary direction of movement
        bool isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.z);

        // Normalize to the cardinal direction
        if (isHorizontal)
        {
            return direction.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            return direction.z > 0 ? Vector3.forward : Vector3.back;
        }
    }
}