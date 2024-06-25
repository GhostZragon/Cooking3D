using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCustomize : MonoBehaviour
{
    [SerializeField] protected MeshFilter meshFilter;
    [SerializeField] protected MeshRenderer meshRenderer;

    protected virtual void OnValidate()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();

    }

    public void SetMaterial(Material mat)
    {
        meshRenderer.SetMaterials(new List<Material> { mat });
    }

    public virtual void SetMesh(Mesh meshToAdd)
    {
        if (meshToAdd == null) return;
        meshFilter.mesh = meshToAdd;
    }
}
