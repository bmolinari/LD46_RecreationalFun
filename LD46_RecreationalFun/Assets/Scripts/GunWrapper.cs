using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWrapper : MonoBehaviour
{
    public List<Weapon> weaponsToSell = new List<Weapon>();

    public Weapon weaponDisplay;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        weaponDisplay = weaponsToSell[Random.Range(0, weaponsToSell.Count)];
        spriteRenderer.sprite = weaponDisplay.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.color = weaponDisplay.GetComponent<SpriteRenderer>().color;
    }

    public void PurchaseWeapon()
    {
        bool purchased = GameManager.instance.PurchaseWeapon(weaponDisplay);
        if(purchased)
        {
            gameObject.SetActive(false);
        }
    }

}
