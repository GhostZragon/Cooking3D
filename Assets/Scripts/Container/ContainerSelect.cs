using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ContainerSelect : MonoBehaviour
{
    public Material normalMat;
    public Material selectMat;
    public MeshRenderer mesh;
    public bool testSelect;

    private void OnValidate()
    {
        if (selectMat == null)
        {
            selectMat = Resources.Load<Material>("SelectMat");
        }
    }

    private void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        normalMat = mesh.material;
    }

    [Button]
    public void SetNormal()
    {
        mesh.material = normalMat;
    }

    [Button]
    public void SetSelect()
    {
        mesh.material = selectMat;
    }
}