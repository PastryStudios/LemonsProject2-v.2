using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    public event EventHandler OnResourceAmountChange;
    [SerializeField] private List<BuildingCost> startingResources;
    [SerializeField] private List<UnitCost> startingUnitResources;

    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
        foreach (BuildingCost resourceAmount in startingResources)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
        TestLogResourceAmountDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {

        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChange?.Invoke(this, EventArgs.Empty);
        // ?. means check the function before the "?." and see if it's null
        // if null, don't run, if not, run

        TestLogResourceAmountDictionary();
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
    public bool CanAfford(BuildingCost[] buildingCostArray)
    {
        foreach(BuildingCost buildingCost in buildingCostArray)
        {
            if(GetResourceAmount(buildingCost.resourceType) >= buildingCost.amount)
            {

            }
            else { return false; }
        }
        return true;
    }
    public void SpendResources(BuildingCost[] buildingCostArray)
    {
        foreach(BuildingCost buildingCost in buildingCostArray)
        {
            resourceAmountDictionary[buildingCost.resourceType] -= buildingCost.amount;
            
        }
    }
    public bool CanAffordUnit(UnitCost[] unitCostArray)
    {
        foreach(UnitCost unitCost in unitCostArray)
        {
            if(GetResourceAmount(unitCost.resourceType) >= unitCost.amount)
            {

            }
            else { return false; }
        }
        return true;
    }
    public void SpendResourcesUnit(UnitCost[] unitCostArray)
    {
        foreach(UnitCost unitCost in unitCostArray)
        {
            resourceAmountDictionary[unitCost.resourceType] -= unitCost.amount;
            
        }
    }

}
