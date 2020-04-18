using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTextController : MonoBehaviour
{
    public TextMeshProUGUI text;
    int currentCombo;

    // Update is called once per frame
    void Update()
    {
        currentCombo = GameManager.instance.CurrentCombo;

        if (currentCombo > 1)
        {
            text.text = $"x{currentCombo}";
        }
        else
        {
            text.text = string.Empty;
        }
    }
}
