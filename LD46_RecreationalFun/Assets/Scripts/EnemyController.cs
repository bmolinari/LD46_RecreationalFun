using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyBrain
{
    Zombie = 0,
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
    public float attackDamage;


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
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
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
        gameObject.SetActive(false);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
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
}
