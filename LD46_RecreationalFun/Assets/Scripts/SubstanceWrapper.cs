using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SubstanceWrapper : MonoBehaviour
{
    public Substance substance;
    public GameObject shopTooltip;

    public Substance standardSubstance;
    public List<Substance> randomSubstances = new List<Substance>();

    public bool randomizeDrink;

    private void OnEnable()
    {
        if(!randomizeDrink)
        {
            InitializeStandardSubstance();
        }
        else
        {
            IntializeRandomSubstance();
        }
    }

    private void InitializeStandardSubstance()
    {
        substance = standardSubstance;
        gameObject.GetComponent<SpriteRenderer>().color = substance.color;
    } 

    private void IntializeRandomSubstance()
    {
        substance = randomSubstances[Random.Range(0, randomSubstances.Count)];
        gameObject.GetComponent<SpriteRenderer>().color = substance.color;
    }

    public void PurchaseSubstance()
    {
        GameManager.instance.PurchaseSubstance(substance);
    }

    public void UpdateTooltip()
    {
        shopTooltip.SetActive(true);
        shopTooltip.GetComponent<ShopTooltip>().SetSubstanceTooltip(substance.name, substance.cost, substance.intoxicationAmount);
    }

    public void HideTooltip()
    {
        shopTooltip.SetActive(false);
    }
}
