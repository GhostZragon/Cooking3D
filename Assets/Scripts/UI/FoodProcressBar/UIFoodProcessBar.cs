using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodProcessBar : MonoBehaviour
{
    [SerializeField] private Image fillImg;
    private void Awake()
    {
    }

    public void SetFillAmount(float amount)
    {
        fillImg.fillAmount = amount;
    }


}
public class UIFoodProcessBarManager : MonoBehaviour
{
    public UIFoodProcessBar foodProcessBarPrefab;
    
}