using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MeshCollider))]

public class FoodCustomizeMesh : MeshCustomize
{
    [SerializeField] private MeshCollider meshCollider;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (meshCollider == null)
            meshCollider = GetComponent<MeshCollider>();
    }
    public override void SetMesh(Mesh meshToAdd)
    {
        base.SetMesh(meshToAdd);
        meshCollider.sharedMesh = meshToAdd;
    }
}
