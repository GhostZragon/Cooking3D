using UnityEngine;
public class SourcePlateContainer : MonoBehaviour, IHolder
{
    [SerializeField] private CookwareType cookwareType;
    [SerializeField] private CookwareDropdown cookwareDropdown;

    private void Start()
    {
        cookwareDropdown.AddListener(OnValueChange);
        cookwareDropdown.SetCookwareType((int)cookwareType);
    }

    public void ExchangeItems(HolderAbstract player)
    {
        if (cookwareType == CookwareType.None) return;
        if (player.IsContainFood() || player.IsContainCookware()) return;
        //Debug.Log("GetFromPool plate");
        var cookware = CookwareManager.instance.GetCookware(cookwareType);
        
        player.SetItem(cookware);
    }
    private void OnValueChange(int i)
    {
        if (i is < 0 or > 4) return;
        cookwareType = (CookwareType)i;
    }
}
