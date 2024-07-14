using System;
using UnityEngine;
using UnityEngine.AI;

public class NpcChair : MonoBehaviour
{
    [SerializeField] private bool IsEmpty;
    [SerializeField] private Transform sitTransform;
    private NPCsTable npcTable;
    public Action OnChangeStateAction;
    public NavMeshObstacle navMeshObstacle;
    private void Awake()
    {
        npcTable = GetComponentInParent<NPCsTable>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
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