using System;
using UnityEngine;

public class NpcChair : MonoBehaviour
{
    [SerializeField] private bool IsEmpty;
    [SerializeField] private Transform sitTransform;
    private NPCsTable npcTable;
    public Action OnChangeStateAction;

    private void Awake()
    {
        npcTable = GetComponentInParent<NPCsTable>();
    }

    public Vector3 GetSitPosition()
    {
        return sitTransform.position;
    }

    public Vector3 GetTableTransform()
    {
        return npcTable.transform.position;
    }

    public void SetIsEmpty(bool b)
    {
        IsEmpty = b;
        OnChangeStateAction?.Invoke();
    }

    public bool GetIsEmpty() => IsEmpty;
}