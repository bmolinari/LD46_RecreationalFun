﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyBrain
{
    Zombie = 0,
    DormantZombie = 1,
    Spitter = 2,
    Queen
}

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public float maxHealth = 10;
    private float currentHealth;

    public float startingMoveSpeed = 4f;
    private float moveSpeed;

    [Header("Spawn Delay")]
    public float spawnDelayMax = 1f;
    private float spawnDelayCounter = 0f;

    [Header("Attack Behavior")]
    public EnemyBrain behavior;
    public GameObject target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackDamage;
    public float maxSpitCooldown = .5f;
    private float spitCooldown;
    private bool startChasing;
    [SerializeField]
    private bool isInRange;

    [Header("Animation")]
    public GameObject deathEffect;

    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Damaged Variables")]
    public float colorFlashTime = .1f;
    public float maxRecoverTime = 1f;
    private Color startingColor;
    private bool isRecoveringFromHit;
    private float currentRecoverTime;

    [Header("Queen")]
    public float maxTeleportCooldown = 10f;
    private float currentTeleportCooldown = 0f;
    public List<Transform> teleportSpots = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        moveSpeed = startingMoveSpeed;
        startingColor = spriteRenderer.color;
        spawnDelayCounter = 0f;
        if (behavior == EnemyBrain.Queen && teleportSpots.Count > 0)
        {
            Teleport();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
        if (spawnDelayCounter < spawnDelayMax)
        {
            spawnDelayCounter += Time.deltaTime;
            return;
        }

        if (behavior == EnemyBrain.Zombie)
        {
            // Mindlessly chase player around map.
            if (target != null)
            {
                MoveTowardsTarget();
            }
        }
        else if(behavior == EnemyBrain.DormantZombie)
        {
            // Chase player once in range
            if (startChasing)
            {
                if (target != null)
                {
                    MoveTowardsTarget();
                }
            }
        }
        else if (behavior == EnemyBrain.Spitter)
        {
            if (spitCooldown < maxSpitCooldown)
            {
                spitCooldown += Time.deltaTime;
            }
            // Shoot bullets at player if in range
            if (isInRange)
            {
                if(spitCooldown >= maxSpitCooldown)
                {
                    Fire();
                }
            }
            else // Otherwise chase the player
            {
                MoveTowardsTarget();
            }
        }
        else if (behavior == EnemyBrain.Queen)
        {
            if(currentTeleportCooldown < maxTeleportCooldown)
            {
                currentTeleportCooldown += Time.deltaTime;
            }
            else
            {
                Teleport();
            }

            if (spitCooldown < maxSpitCooldown)
            {
                spitCooldown += Time.deltaTime;
            }
            // Shoot bullets at player if in range
            if (isInRange)
            {
                if (spitCooldown >= maxSpitCooldown)
                {
                    Fire();
                }
            }
            else // Otherwise chase the player
            {
                MoveTowardsTarget();
            }
        }


        if (isRecoveringFromHit)
        {
            currentRecoverTime += Time.deltaTime;

            if (currentRecoverTime >= colorFlashTime)
            {
                spriteRenderer.color = startingColor;
            }

            if (currentRecoverTime >= maxRecoverTime)
            {
                isRecoveringFromHit = false;
                moveSpeed = startingMoveSpeed;
            }
        }
    }

    public void Teleport()
    {
        Vector3 teleportSpot = teleportSpots[Random.Range(0, teleportSpots.Count)].position;

        while(teleportSpot == transform.position)
        {
            teleportSpot = teleportSpots[Random.Range(0, teleportSpots.Count)].position;
        }

        transform.position = teleportSpot;
        currentTeleportCooldown = 0;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        isRecoveringFromHit = true;
        moveSpeed = 2f;
        currentRecoverTime = 0;
        spriteRenderer.color = GetRandomColor();
    }

    public void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        if (behavior == EnemyBrain.Queen)
        {
            GameManager.instance.GameWon();
        }
        else
        {
            GameManager.instance.RemoveTrackedEnemy(gameObject);
            
        }
        AudioManager.instance.PlayDeath();
        Destroy(gameObject);
    }

    public void SetRandomColor()
    {
        Color newColor = new Color(
              Random.Range(.5f, .9f),
              Random.Range(0f, .4f),
              Random.Range(0f, .4f)
          );
        startingColor = newColor;
        spriteRenderer.color = newColor;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerToxicity>().BuzzKill(attackDamage);
            moveSpeed = 0;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            moveSpeed = startingMoveSpeed;
        }
    }

    public void StartChasingTarget()
    {
        startChasing = true;
    }

    public void SetInRange(bool value)
    {
        isInRange = value;
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Bullet>().SetDamage(attackDamage);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(firePoint.right * 20, ForceMode2D.Impulse);
        spitCooldown = 0;
        AudioManager.instance.PlayButtonClip();
    }

    private void RotateTowardsTarget()
    {
        Vector2 tar = target.transform.position;

        Vector2 position = transform.position;
        tar.x = tar.x - position.x;
        tar.y = tar.y - position.y;

        float angle = Mathf.Atan2(tar.y, tar.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    private Color GetRandomColor()
    {
        Color newColor = new Color(
              Random.Range(0f, 1f),
              Random.Range(0f, 1f),
              Random.Range(0f, 1f)
          );

        return newColor;
    }
}
