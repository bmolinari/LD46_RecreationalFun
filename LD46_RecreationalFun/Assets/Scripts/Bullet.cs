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
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
        
        
    }
}