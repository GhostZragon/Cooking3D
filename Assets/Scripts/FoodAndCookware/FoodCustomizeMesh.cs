using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class FoodCustomizeMesh : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;

    private void OnValidate()
    {
        if (meshCollider == null)
            meshCollider = GetComponent<MeshCollider>();

        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();


    }
    public void SetMaterial(Material mat)
    {
        meshRenderer.SetMaterials(new List<Material> { mat });
    }
    public void SetMesh(Mesh meshToAdd)
    {
        if (meshToAdd == null) return;
        meshCollider.sharedMesh = meshToAdd;
        meshFilter.mesh = meshToAdd;
    }
}
