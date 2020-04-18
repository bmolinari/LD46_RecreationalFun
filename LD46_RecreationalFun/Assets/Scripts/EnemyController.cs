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
    private Rigidbody2D rb;

    private bool isRecoveringFromHit;
    public float maxRecoverTime = 1f;
    private float currentRecoverTime;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        moveSpeed = startingMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(behavior == EnemyBrain.Zombie)
        {
            // Mindlessly chase player around map.
            if(target != null)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            }
        }


        if(isRecoveringFromHit)
        {
            currentRecoverTime += Time.deltaTime;
            if(currentRecoverTime >= maxRecoverTime)
            {
                isRecoveringFromHit = false;
                moveSpeed = startingMoveSpeed;
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        isRecoveringFromHit = true;
        moveSpeed = 2f;
        currentRecoverTime = 0;
    }
}
