﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SubstanceWrapper : MonoBehaviour
{
    public Substance substance;

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
    } 

    private void IntializeRandomSubstance()
    {
        // get from random list
    }

    public void PurchaseSubstance()
    {
        GameManager.instance.PurchaseSubstance(substance);
    }
}
