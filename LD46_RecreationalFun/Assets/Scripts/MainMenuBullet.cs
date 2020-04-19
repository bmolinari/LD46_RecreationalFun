using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBullet : MonoBehaviour
{
    public float damage;
    public GameObject impactEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                Destroy(gameObject);
                break;
            case "Wall":
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                Destroy(gameObject);
                break;
        }
    }
}