using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Cookware Data",menuName ="ScriptableObjects/CookwareData")]
public class CookwareData : ScriptableObject
{
    [Serializable]
    public struct CoowareModelData
    {
        public CoowareModelData(CookwareType type)
        {
            this.type = type;
            this.model = null;
        }
        public Mesh model;
        public CookwareType type;
    }
    public List<CoowareModelData> list;
    [Button]
    private void InitRawList()
    {
        list.Clear();
        list = new List<CoowareModelData>()
        {
            new CoowareModelData(CookwareType.Plate),
            new CoowareModelData(CookwareType.Pan),
            new CoowareModelData(CookwareType.Pot)
        };
    }

    public Mesh GetMeshByType(CookwareType type)
    {
        foreach(var item in list)
        {
            if(item.type == type)
            {
                return item.model;
            }
        }
        return null;
    }
}
