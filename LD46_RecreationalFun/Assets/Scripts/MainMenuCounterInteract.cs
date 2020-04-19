using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCounterInteract : MonoBehaviour
{
    public MainMenuManager menu;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            menu.ShowGreeting();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            menu.HideToolTip();
        }
    }
}
