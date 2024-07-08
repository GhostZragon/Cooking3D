using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class NPCsTable : MonoBehaviour
{
    public List<NpcChair> freeChairList;
    private int chairCanUseCount;

    private void Awake()
    {
        chairCanUseCount = freeChairList.Count;
        foreach (var item in freeChairList)
        {
            item.OnChangeStateAction = OnChangeStateOfList;
        }
        OnChangeStateOfList();
    }

    public NpcChair GetEmptyChair()
    {
        foreach (var item in freeChairList)
        {
            if (item.GetIsEmpty())
            {
                chairCanUseCount--;
                return item;
            }
        }

        return null;
    }

    private void OnChangeStateOfList()
    {
        foreach (var item in freeChairList)
        {
            if (item.GetIsEmpty())
            {
                IsHaveTable = true;
                return;
            }
        }

        IsHaveTable = false;
    }

    public bool IsHaveTable;
}