using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopTooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI messageText;


    public void SetSubstanceTooltip(string name, int cost, int toxicityAmount)
    {
        nameText.text = name;
        costText.text = cost.ToString();
        descriptionText.text = $"+{toxicityAmount} Intoxication";
        messageText.text = string.Empty;
    }

    public void SetWeaponTooltip(string name, int cost, int maxAmmo, FireType type)
    {
        nameText.text = name;
        costText.text = cost.ToString();
        descriptionText.text = $"{type.ToString()} | Max Ammo:{maxAmmo}";
        messageText.text = string.Empty;
    }

    public void SetCloseShopTooltip(string message)
    {
        nameText.text = string.Empty;
        costText.text = string.Empty;
        descriptionText.text = string.Empty;
        messageText.text = message;
    }

    public void FinalWaveMessage()
    {
        nameText.text = string.Empty;
        costText.text = string.Empty;
        descriptionText.text = string.Empty;
        messageText.text = "Hey uh..you're not looking too good. This should be the last of 'em.";
    }

    public void WinMessage()
    {
        messageText.text = "I can't believe you actually did it. Come back and drink for free whenever you want. Cheers!";
        nameText.text = string.Empty;
        costText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}
