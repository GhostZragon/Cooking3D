using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public Customer Prefab;
    public NPCsTable freeTable;
    [Button]
    private void Spawner()
    {
        var target = freeTable.GetEmptyChair();
        if (target == null) return;
        Prefab.SetDestination(target.transform);
    }

}
