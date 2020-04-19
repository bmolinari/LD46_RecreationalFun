using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public KeyCode interactKey;
    public UnityEvent action;
    public bool needsKeyPress = true;

    private SpriteRenderer spriteRenderer;
    private Color startingColor;
    private Color highlightedColor;
    private bool isInRange;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Interact Key: {interactKey.ToString()}");
        if (spriteRenderer != null)
        {
            startingColor = spriteRenderer.color;
            highlightedColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetKeyDown(interactKey))
        {
            if (action != null)
            {
                action.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (needsKeyPress)
            {
                isInRange = true;
                spriteRenderer.color = highlightedColor;
            }
            else
            {
                action.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (needsKeyPress)
            {
                isInRange = false;
                spriteRenderer.color = startingColor;
            }
        }
    }
}
