using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    public readonly static string Idle = "Idle";
    public readonly static string Interact = "Interact";
    public readonly static string Running = "Running_A";
    public readonly static string SitChairDown = "Sit_Chair_Down";
    public readonly static string SitChairIdle = "Sit_Chair_Idle";
    public readonly static string SitChairStandUp = "Sit_Chair_StandUp";
    public readonly static string Walking = "Walking_A";

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
}
