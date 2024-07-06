using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCsTable : MonoBehaviour
{
    public List<NpcChair> chairList;

    public NpcChair GetEmptyChair()
    {
        foreach(var item in chairList)
        {
            if (item.IsEmpty)
                return item;
        }
        return null;
    }
}
