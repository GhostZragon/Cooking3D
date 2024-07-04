using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcePlateContainer : SourceCookwareContainer
{
    private int plateCount = 4;
    private int maxPlateCount = 4;
    [SerializeField] private GameObject[] plateObjModel;
    public override void ExchangeItems(HolderAbstract player)
    {
        if (plateCount == 0)
            return;

        base.ExchangeItems(player);
        AddCallback(player.GetCookware());

    }

    private void AddCallback(Cookware cookware)
    {
        if (cookware == null) return;

        if (cookware.GetCookwareType() == CookwareType.Plate)
        {
            cookware.SetOnPlateDiscardCallback(OnDiscardDisk);
            plateCount--;
            UpdatePlateCount();
        }
    }
    private void UpdatePlateCount()
    {
        for(int i = 0; i < maxPlateCount; i++)
        {
            if (plateObjModel[i].transform.GetSiblingIndex() < plateCount)
            {
                plateObjModel[i].gameObject.SetActive(true);
            }
            else
            {
                plateObjModel[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnDiscardDisk()
    {
        plateCount++;
        UpdatePlateCount();
    }
}
