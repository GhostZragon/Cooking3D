using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public interface ITriggerProcress
{
    bool OnTrigger { get; set; }
}
public class ContainerSelect : MonoBehaviour, ITriggerProcress
{
    [SerializeField] private Material normalMat;
    [SerializeField] private Material selectMat;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private bool testSelect;
    public bool OnTrigger { get; set; }

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
        mesh.sharedMaterial = normalMat;
        OnTrigger = false;
    }

    [Button]
    public void SetSelect()
    {
        mesh.sharedMaterial = selectMat;
        OnTrigger = true;
    }
}