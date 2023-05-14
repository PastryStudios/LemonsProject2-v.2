using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]

public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public BuildingCost[] buildingCostArray;
    public int maxHealth;
    public bool hasResourceGeratorData;
    public float attackTimerDelayMax = .1f;
    public float bulletDelayMax = .1f;

    public string GetConstructionCostString()
    {
        string str = "";
        foreach(BuildingCost buildingCost in buildingCostArray)
        {
            str +=  "<color=#" + buildingCost.resourceType.colourHex + ">" + 
                buildingCost.resourceType.shortName + buildingCost.amount + "</color> ";
        }
        return str;
    }
}
