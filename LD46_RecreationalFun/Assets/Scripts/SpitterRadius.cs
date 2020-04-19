using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterRadius : MonoBehaviour
{
    public EnemyController spitterController;

    private void Awake()
    {
        spitterController = transform.parent.gameObject.GetComponent<EnemyController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spitterController.SetInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spitterController.SetInRange(false);
        }
    }
}
