using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuWeapon : MonoBehaviour
{
    [Header("Shop Details")]
    public string weaponName;
    public int cost;

    [Header("Weapon Properties")]
    public Transform firePoint;
    public float weaponDamage = 2;
    public FireType fireType;
    public int burstAmount = 3;
    public float maxBurstFireCooldown = 1f;
    public float maxAutomaticFireCooldown = .2f;
    public bool limitedAmmo;
    public int maxAmmo = 100;

    [Header("Projectile Properties")]
    public GameObject bulletPrefab;
    public float fireForce = 20f;

    private int currentAmmo;
    private bool isShooting;
    private float currentBurstFireCooldown;
    private float currentAutomaticFireCooldown;

    private void Start()
    {
        if (limitedAmmo)
        {
            currentAmmo = maxAmmo;
        }

        if (fireType == FireType.Burst)
        {
            currentBurstFireCooldown = maxBurstFireCooldown;
        }

        if (fireType == FireType.Automatic)
        {
            currentAutomaticFireCooldown = maxAutomaticFireCooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireType == FireType.SemiAutomatic)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
        else if (fireType == FireType.Burst)
        {
            if (currentBurstFireCooldown <= maxBurstFireCooldown)
            {
                currentBurstFireCooldown += Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!isShooting)
                    {
                        StartCoroutine(FireBullets(burstAmount));

                    }
                }
            }
        }
        else if (fireType == FireType.Automatic)
        {
            if (currentAutomaticFireCooldown <= maxAutomaticFireCooldown)
            {
                currentAutomaticFireCooldown += Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Fire();
                }
            }
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        if (limitedAmmo)
        {
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                GameManager.instance.ReturnDefaultWeaponToPlayer();
            }
        }
        if (fireType == FireType.Automatic)
            currentAutomaticFireCooldown = 0;
    }

    private IEnumerator FireBullets(int amount)
    {
        int shotsFired = 0;
        isShooting = true;
        while (shotsFired < amount)
        {
            Fire();
            shotsFired++;
            yield return new WaitForSeconds(.1f);
        }
        currentBurstFireCooldown = 0;
        isShooting = false;
    }
}
