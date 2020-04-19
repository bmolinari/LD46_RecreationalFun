using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyBrain
{
    Zombie = 0,
    DormantZombie = 1,
    Spitter = 2,
}

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public float maxHealth = 10;
    private float currentHealth;

    public float startingMoveSpeed = 4f;
    private float moveSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
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
            RotateTowardsTarget();
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
        GameManager.instance.RemoveTrackedEnemy(gameObject);
        gameObject.SetActive(false);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
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
