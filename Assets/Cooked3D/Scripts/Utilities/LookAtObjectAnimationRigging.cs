using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtObjectAnimationRigging : MonoBehaviour
{
    private Rig rig;
    [SerializeField] private float weightValue;
    [SerializeField] private float speed = 10;
    private YieldInstruction yieldWaitForFrame;

    private void Awake()
    {
        rig = GetComponent<Rig>();
        yieldWaitForFrame = new WaitForEndOfFrame();
    }

    [Button]
    public void UnWeight()
    {
        RunCoroutine(0);
    }

    [Button]
    public void SetWeight()
    {
        RunCoroutine(1);
    }

    private void RunCoroutine(float value)
    {
        weightValue = value;
        StopAllCoroutines(); // Stop any running coroutine before starting a new one
        StartCoroutine(StartWeightLerp());
    }

    private IEnumerator StartWeightLerp()
    {
        var startValue = rig.weight;
        while (!Mathf.Approximately(rig.weight, weightValue))
        {
            rig.weight = Mathf.Lerp(rig.weight, weightValue, Time.deltaTime * speed);
            if (Mathf.Abs(rig.weight - weightValue) < 0.001f)
            {
                break;
            }
            yield return yieldWaitForFrame;
        }
        rig.weight = weightValue; // Ensure the final value is set
    }
}
