using UnityEngine;
public class SourcePlateContainer : MonoBehaviour, IHolder
{
    [SerializeField] private CookwareType cookwareType;
    private CookwareManager cookwareManager;
    private static int plateCount = 3;
    private void Start()
    {


        cookwareManager = ServiceLocator.Current.Get<CookwareManager>();
    }

    public void ExchangeItems(HolderAbstract player)
    {
        if (cookwareType == CookwareType.None) return;
        if (player.IsContainFood() || player.IsContainCookware()) return;
        //Debug.Log("GetFromPool plate");


        var cookware = cookwareManager.GetCookware(cookwareType);
        
        if(cookware.GetCookwareType() == CookwareType.Plate)
        {
            cookware.SetOnPlateDiscardCallback(OnDiscardDisk);
            plateCount++;
        }

        player.SetItem(cookware);
    }

    private void OnDiscardDisk()
    {
        plateCount--;
    }


}
