using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToxicity : MonoBehaviour
{
    [Header("Toxicity Values")]
    public float maxToxicityLevels = 1000;
    public float minToxicityLevels = 0;
    private float currentToxicityLevels;

    public bool isSober;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Damaged Variables")]
    public float colorFlashTime = .1f;
    public float maxRecoverTime = 1f;
    private Color startingColor;
    private bool isRecoveringFromHit;
    private float currentRecoverTime;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        startingColor = spriteRenderer.color;
    }

    public void Start()
    {
        currentToxicityLevels = 100f;
    }

    private void Update()
    {
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
            }
        }
        else
        {
            if (!isSober)
            {
                currentToxicityLevels -= Time.deltaTime;
            }
            else
            {
                currentToxicityLevels = minToxicityLevels;
            }
        }


        
    }


    public void BuzzKill(float amount)
    {
        if (isSober)
        {
            Die();
        }
        else
        {
            if (!isRecoveringFromHit)
            {
                currentToxicityLevels -= amount;
                if (currentToxicityLevels <= minToxicityLevels)
                {
                    currentToxicityLevels = minToxicityLevels;
                    isSober = true;
                }
                isRecoveringFromHit = true;
                currentRecoverTime = 0;
                spriteRenderer.color = GetHitColor();
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private Color GetHitColor()
    {
        Color newColor = new Color(
              0,
              1,
              1,
              1
          );

        return newColor;
    }
}
