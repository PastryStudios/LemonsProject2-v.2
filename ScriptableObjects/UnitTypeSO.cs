using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitType")]

public class UnitTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Sprite sprite;
    public UnitCost[] unitCostArray;
    public int maxHealth;
    public float attackTimerDelayMax = .1f;
    public float bulletDelayMax = .1f;
    public float attackRadius = 2f;


    public string GetConstructionCostString()
    {
        string str = "";
        foreach (UnitCost unitCost in unitCostArray)
        {
            str += "<color=#" + unitCost.resourceType.colourHex + ">" +
                unitCost.resourceType.shortName + unitCost.amount + "</color> ";
        }
        return str;
    }
}
