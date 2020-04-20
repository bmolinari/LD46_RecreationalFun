using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject impactEffect;
    public bool harmsPlayer;


    public void SetDamage(float weaponDamage)
    {
        damage = weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                
                if (!harmsPlayer)
                {
                    if (impactEffect != null)
                    {
                        Instantiate(impactEffect, transform.position, transform.rotation);
                    }
                    AudioManager.instance.PlayHit();
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                    GameManager.instance.IncreaseComboCount();
                    Destroy(gameObject);
                }
                break;
            case "BugNest":
                
                if (!harmsPlayer)
                {
                    if (impactEffect != null)
                    {
                        Instantiate(impactEffect, transform.position, transform.rotation);
                    }
                    AudioManager.instance.PlayHit();
                    collision.gameObject.GetComponent<BugNestController>().TakeDamage(damage);
                    GameManager.instance.IncreaseComboCount();
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                if(impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                if (!harmsPlayer)
                {
                    GameManager.instance.ResetComboCount();
                }
                AudioManager.instance.PlayHit();
                Destroy(gameObject);
                break;
            case "Player":
                if (harmsPlayer)
                {
                    AudioManager.instance.PlayHit();
                    collision.gameObject.GetComponent<PlayerToxicity>().BuzzKill(damage);
                }
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                
                Destroy(gameObject);
                break;
        }
    }
}