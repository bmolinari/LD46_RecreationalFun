using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Substance : ScriptableObject
{
    public string name;
    public int cost;
    public int intoxicationAmount;
    public Color color;
}
