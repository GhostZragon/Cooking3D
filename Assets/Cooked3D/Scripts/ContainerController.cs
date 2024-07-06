using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private List<HolderAbstract> holdersList;
    [SerializeField] private List<SourceFoodContainer> sourceFoodContainers;
    [Header("Parent")]
    [SerializeField] private GameObject holderParent;
    [SerializeField] private GameObject sourceFoodParent;

    private const string holderPrefix = "Holder";
    private const string sourceFoodPrefix = "SourceFood";
#if UNITY_EDITOR

    [SerializeField] private bool runInStart;

    private void Start()
    {
        if (runInStart)
        {
            Rename();
        }
    }
#endif

    [Button]
    private void Rename()
    {
        InitParents();
        RenameList(ref holdersList, holderPrefix, holderParent);
        RenameList(ref sourceFoodContainers, sourceFoodPrefix, sourceFoodParent);
    }
    private void RenameList<T>(ref List<T> list, string prefix, GameObject parent = null) where T : MonoBehaviour
    {
        if (list == null) return;
        
        list.Clear();
        bool haveParent = parent != null ? true : false;
        int count = 1;
        
     
        foreach(var item in GetComponentsInChildren<T>())
        {
            // if parent not null, reparent it
            if (haveParent) item.transform.parent = parent.transform;
            // Rename and add it to list
            item.name = $"{prefix}_{count}";
            list.Add(item);
            count++;
        }
    }
    private void InitParents()
    {
        holderParent = InitParent(holderParent, "Holder Parent");
        sourceFoodParent = InitParent(sourceFoodParent, "Source Food Parent");
    }
    private GameObject InitParent(GameObject objParent,string parentName)
    {
        if(objParent == null)
        {
            objParent = new GameObject();
            objParent.name = parentName;
            objParent.transform.parent = transform;
        }
        return objParent;
    }
    [Button]
    private void AddPlaceTransform()
    {
        foreach(var item in holdersList)
        {
            if(item is Container)
            {
                var container = item as Container;
                container.LoadPlaceTransform();
            }
        }
    }
}

