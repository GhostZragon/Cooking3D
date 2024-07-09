using System.Collections;
using UnityEngine;

public static class MAnimator
{
    /// <summary> 
    /// Changes current animation smoothly. <br></br>
    /// <b>exitInTransition = true:</b> Will stop if animator is in a transition.
    /// </summary>
    public static void WaitPlay(this Animator animator, string animation, float speed, bool exitInTransition = true)
    {
        if (exitInTransition)
        {
            if (!animator.IsInTransition(0)) animator.CrossFade(animation, speed);
        }
        else animator.CrossFade(animation, speed);
    }

    /// <summary> 
    /// Changes current animation even when animator is in a transition.
    /// </summary>
    public static void ImmediatePlay(this Animator animator, string animation, float speed)
        => animator.WaitPlay(animation, speed, false);

    /// <summary>
    /// Just like <see cref="WaitPlay(Animator, string, float, bool)"/> but it can replay the same playing animation.
    /// </summary>
    public static void SamePlay(this Animator animator, string animation, float speed)
        => animator.CrossFadeInFixedTime(animation, speed);

    /// <summary>
    /// Just like <see cref="Animator.Play(string, int, float)"/>, play an animation in a harsh way.
    /// </summary>
    public static void HarshPlay(this Animator animator, string animation, int layer = -1)
        => animator.Play(animation, layer, 0);

    private static YieldInstruction yieldFrame = new WaitForEndOfFrame();
    public static IEnumerator PlayAndWait(this Animator animator, string animation, float speed)
    {
        animator.Play(animation);
        yield return yieldFrame;
        // Get the length of the animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        Debug.Log("Animation time: "+animationLength);
        // Wait for the animation to complete
        yield return new WaitForSeconds(animationLength);
    }
}
