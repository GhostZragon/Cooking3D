using UnityEngine;

public abstract class PickUpAbtract : MonoBehaviour
{
    public void SetToParentAndPosition(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }
    public abstract void Discard();
}