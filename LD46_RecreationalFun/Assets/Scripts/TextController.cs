using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum UITextType
{
    Combo,
    Coin
}


public class TextController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public UITextType type;
    int currentCount;

    // Update is called once per frame
    void Update()
    {
        if (type == UITextType.Combo)
        {
            currentCount = GameManager.instance.CurrentCombo;
            if (currentCount > 1)
            {
                text.text = $"x{currentCount}";
            }
            else
            {
                text.text = string.Empty;
            }
        } 
        else if (type == UITextType.Coin)
        {
            currentCount = GameManager.instance.CurrentCoinCount;
            text.text = $"${currentCount}";
        }
    }
}
