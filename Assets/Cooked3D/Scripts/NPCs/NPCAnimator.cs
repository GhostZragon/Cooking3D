using NaughtyAttributes;
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

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    [Button]
    private void Test()
    {
        PlayAnim(Walking);
    }

    public void PlayAnim(string animName)
    {
        Debug.Log("Animator NPC play animation name: " + animName);
        MAnimator.HarshPlay(animator, animName);
    }
    public void WaitPlay(string animName)
    {
        Debug.Log("Animator NPC play animation name: " + animName);
        MAnimator.WaitPlay(animator, animName,.2f);
    }
}
