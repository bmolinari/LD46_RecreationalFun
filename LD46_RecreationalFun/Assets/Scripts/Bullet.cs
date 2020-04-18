using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject impactEffect;


    public void SetDamage(float weaponDamage)
    {
        damage = weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                // Increase Combo Counter
                Destroy(gameObject);
                break;
            default:
                if(impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                // Reset Combo Counter
                Destroy(gameObject);
                break;
        }
        
        
    }
}