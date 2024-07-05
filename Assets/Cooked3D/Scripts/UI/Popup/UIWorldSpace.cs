using UnityEngine;

[RequireComponent(typeof(UIHoldWorldPosition))]
public class UIWorldSpace : MonoBehaviour
{
    protected UIHoldWorldPosition UIHoldWorldPosition;

    protected virtual void Awake()
    {
        UIHoldWorldPosition = GetComponent<UIHoldWorldPosition>();
    }

    public void SetStandPosition(Vector3 worldPosition)
    {
        if(UIHoldWorldPosition == null)
        {
            Debug.Log("null");
            return;
        }
        UIHoldWorldPosition.SetStandPosition(worldPosition);
    }

}
