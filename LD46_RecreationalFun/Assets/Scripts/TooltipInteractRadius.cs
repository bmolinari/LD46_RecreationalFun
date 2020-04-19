using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TooltipInteractRadius : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.parent.gameObject.activeInHierarchy)
            {
                transform.parent.gameObject.SendMessage("UpdateTooltip");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.parent.gameObject.activeInHierarchy)
            {
                transform.parent.gameObject.SendMessage("HideTooltip");
            }
        }
    }
}
