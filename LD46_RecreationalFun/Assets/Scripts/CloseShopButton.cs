using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShopButton : MonoBehaviour
{
    public GameObject shopTooltip;

    public void UpdateTooltip()
    {
        if (!GameManager.instance.prepareForFinalWave)
        {
            shopTooltip.SetActive(true);
            shopTooltip.GetComponent<ShopTooltip>().SetCloseShopTooltip("Close shop and continue?");
        }
    }

    public void HideTooltip()
    {
        shopTooltip.SetActive(false);
    }
}
