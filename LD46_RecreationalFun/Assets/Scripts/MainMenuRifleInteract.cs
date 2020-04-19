using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRifleInteract : MonoBehaviour
{
    public MainMenuManager menu;
    public bool isInRange;
    private void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                menu.EquipRifle();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            menu.ShowRifleTip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            menu.HideToolTip();
        }
    }

}
