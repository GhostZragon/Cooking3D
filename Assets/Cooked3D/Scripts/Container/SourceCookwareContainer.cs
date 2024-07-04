using UnityEngine;
public class SourceCookwareContainer : MonoBehaviour, IHolder
{
    [SerializeField] private CookwareType cookwareType;
    private CookwareManager cookwareManager;
    private void Start()
    {
        cookwareManager = ServiceLocator.Current.Get<CookwareManager>();
    }

    public virtual void ExchangeItems(HolderAbstract player)
    {
        if (cookwareType == CookwareType.None) return;
        if (player.IsContainFood() || player.IsContainCookware()) return;
        //Debug.Log("GetFromPool plate");


        var cookware = cookwareManager.GetCookware(cookwareType);
     

        player.SetItem(cookware);
    }


}
