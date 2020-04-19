using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSubstanceInteract : MonoBehaviour
{
    public MainMenuManager menu;
    public bool isInRange;
    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                menu.StartGame();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            menu.ShowDrinkMessage();
        }
    }
}
