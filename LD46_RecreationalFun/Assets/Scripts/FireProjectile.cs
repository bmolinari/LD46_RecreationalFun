using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [Header("Weapon Properties")]
    public Transform firePoint;
    public float weaponDamage = 2;

    [Header("Projectile Properties")]
    public GameObject bulletPrefab;
    public float fireForce = 20f;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Bullet>().SetDamage(weaponDamage);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }
}
