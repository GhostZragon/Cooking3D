using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLayoutCustom : MonoBehaviour
{
    public List<RectTransform> rects;
    public float animationDuration = 1.0f;
    public Vector3 StartPos;
    public float XSpace;
    [Button]
    private void StartTest()
    {
        StartCoroutine(AnimateElements());
    }
    IEnumerator AnimateElements()
    {
        for (int i = 0; i < rects.Count; i++)
        {
            yield return AnimateElement(rects[i], i + 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator AnimateElement(RectTransform element, int index)
    {
        element.transform.localPosition = StartPos;
        Vector3 startPosition = element.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x + index * XSpace, startPosition.y, startPosition.z);

        float elapsedTime = 0;

        yield return element.DOLocalMove(targetPosition,animationDuration).WaitForCompletion();
    }
}

